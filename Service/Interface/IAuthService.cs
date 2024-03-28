using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Common;
using BusinessObject.Dto.Account;
using BusinessObject.Dto.Auth;

namespace Service.Interface
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponse>> Login(LoginRequest request);
        Task<ApiResponse<string>> Signup(SignupRequest request);
    }
}
