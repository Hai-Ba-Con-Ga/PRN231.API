using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.CollectData;

namespace Service.Interface;

public interface IDatumService
{
    Task<PagingApiResponse<DatumResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
    Task<ApiResponse<DatumResponse>> GetDatum(int id);
    Task<ApiResponse<bool>> CreateAsync(DatumRequest request);
    Task<ApiResponse<bool>> UpdateAsync(int id, DatumRequest request);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    
}