using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;

namespace Service.Interface;

public interface IDeviceTypeService
{
    Task<PagingApiResponse<DeviceTypeResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
    Task<ApiResponse<DeviceTypeResponse>> GetDeviceType(int id);
    Task<ApiResponse<bool>> CreateAsync(CreateDeviceTypeRequest request);
    Task<ApiResponse<bool>> UpdateAsync(int id, CreateDeviceTypeRequest request);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}