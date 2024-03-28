using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Common;
using BusinessObject.Dto.Account;
using BusinessObject.Dto.Auth;
using BusinessObject.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement
{
    public class AuthService : BaseService, IAuthService
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        private readonly SigningCredentials _credentials;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        //login
        public async Task<ApiResponse<LoginResponse>> Login(LoginRequest request)
        {
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(request.Username));
            // Check if username is existed
            if (account == null)
                return Failed<LoginResponse>("Username is not existed", System.Net.HttpStatusCode.BadRequest);
            // Check if password is correct
            if (!VerifyPassword(account.Password, request.Password))
                return Failed<LoginResponse>("Password is incorrect", System.Net.HttpStatusCode.BadRequest);
            // generate token
            var token = GenerateToken(account);

            return Success(new LoginResponse()
            {
                AccessToken = token,
                AccountId = account.AccountId
            });
        }

        //register
        public async Task<ApiResponse<string>> Signup(SignupRequest request)
        {
            throw  new NotImplementedException();
        }


        private string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        private bool VerifyPassword(string passwordHash, string inputPassword)
        {
            var parts = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);
            var newHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, _hashAlgorithmName, KeySize);
            return newHash.SequenceEqual(hash);
        }

        //generate token
        private string GenerateToken(Account account)
        {
            //create token
            var claimIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString())
            });
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Expires = DateTime.UtcNow.AddHours(AppConfig.JwtSetting.AccessTokenExpiration),
                SigningCredentials = _credentials,
                Issuer = AppConfig.JwtSetting.ValidIssuer,
                Audience = AppConfig.JwtSetting.ValidAudience,
                IssuedAt = DateTime.UtcNow
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}