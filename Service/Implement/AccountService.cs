using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using BusinessObject.Common;
using BusinessObject.Common.Enums;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.Account;
using BusinessObject.Model;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement;

public class AccountService : BaseService, IAccountService
{
    private const int SaltSize = 128 / 8;
    private const int KeySize = 256 / 8;
    private const int Iterations = 10000;
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private const char Delimiter = ';';
    private readonly SigningCredentials _credentials;
    private readonly SymmetricSecurityKey _securityKey;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public AccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.JwtSetting.IssuerSigningKey));
        _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    public async Task<ApiResponse<string>> Create(CreateAccountRequest req)
    {
        try
        {
            //check if username is existed
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.Username.Equals(req.Username));
            if (account != null)
                throw new ArgumentException("Username is existed in system already!");
            account = req.Adapt<Account>();
            //hash password
            account.Password = HashPassword(req.Password);
            account.Role = req.Role.ToString().ToUpper();
            await _unitOfWork.Resolve<Account>().CreateAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return Success<string>("Create account successfully!");
        }
        catch (Exception ex)
        {
            return Failed<string>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<AccountResponse>> GetById(int accountId)
    {
        try
        {
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.AccountId == accountId);
            if (account == null)
                throw new ArgumentException("Account is not existed");
            var response = account.Adapt<AccountResponse>();
            return Success(response);
        }
        catch (Exception ex)
        {
            return Failed<AccountResponse>(ex.GetExceptionMessage());
        }
    }

    //get all
    public async Task<PagingApiResponse<AccountResponse>> Search(string? keySearch, PagingQuery? pagingQuery,
        string? orderByStr)
    {
        try
        {
            var response = await _unitOfWork.Resolve<Account>()
                .SearchAsync<AccountResponse>(keySearch, pagingQuery, orderByStr);
            return Success(response);
        }
        catch (Exception ex)
        {
            return PagingFailed<AccountResponse>(ex.GetExceptionMessage());
        }
    }

    //update account
    public async Task<ApiResponse<string>> Update(int accountId, UpdateAccountRequest req)
    {
        try
        {
            var account = await _unitOfWork.Resolve<Account>()
                .FindAsync(x => x.AccountId == accountId);
            if (account == null)
                throw new ArgumentException("Account is not existed");
            account.Role = req.Role;
            account.Email = req.Email;
            account.FirstName = req.FirstName;
            account.LastName = req.LastName;
            account.BirthDate = req.BirthDate;
            if (req.ParentAccountId != null) account.ParentAccountId = req.ParentAccountId;

            await _unitOfWork.SaveChangesAsync();
            return Success<string>("Update account successfully!");
        }
        catch (Exception ex)
        {
            return Failed<string>(ex.GetExceptionMessage());
        }
    }
    //delete account
    public async Task<ApiResponse<string>> Delete(int accountId)
    {
        try
        {
            var account = await _unitOfWork.Resolve<Account>().FindAsync(x => x.AccountId == accountId);
            if (account == null)
                throw new ArgumentException("Account is not existed");
            await _unitOfWork.Resolve<Account>().DeleteAsync(account);
            await _unitOfWork.SaveChangesAsync();
            return Success<string>("Delete account successfully!");
        }
        catch (Exception ex)
        {
            return Failed<string>(ex.GetExceptionMessage());
        }
    }


    private string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }
}