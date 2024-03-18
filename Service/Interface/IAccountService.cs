using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.Account;

namespace Service.Interface;

public interface IAccountService
{
    Task<ApiResponse<string>> Create(CreateAccountRequest req);
    Task<ApiResponse<AccountResponse>> GetById(int accountId);

    Task<PagingApiResponse<AccountResponse>> Search(string? keySearch, PagingQuery? pagingQuery,
        string? orderByStr);
    Task<ApiResponse<string>> Update(int accountId, UpdateAccountRequest req);
    Task<ApiResponse<string>> Delete(int accountId);
}