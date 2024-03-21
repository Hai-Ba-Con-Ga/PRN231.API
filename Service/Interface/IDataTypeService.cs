using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.CollectData;
using BusinessObject.Dto.DeviceType;

namespace Service.Interface;

public interface IDataTypeService
{
    Task<PagingApiResponse<DataTypeResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
    Task<ApiResponse<DataTypeResponse>> GetDeviceType(int id);
    Task<ApiResponse<bool>> CreateAsync(DataTypeRequest request);
    Task<ApiResponse<bool>> UpdateAsync(int id, DataTypeRequest request);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}