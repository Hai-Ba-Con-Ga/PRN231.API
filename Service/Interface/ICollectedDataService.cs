using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.CollectData;

namespace Service.Interface;

public interface ICollectedDataService
{
    Task<PagingApiResponse<DataResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
    Task<ApiResponse<DataResponse>> GetCollectedData(int id);
    Task<ApiResponse<bool>> CreateAsync(CollectedDataRequest request);
    Task<ApiResponse<bool>> UpdateAsync(int id, CollectedDataRequest request);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    
}