using BusinessObject.Common.PagedList;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;

namespace Service.Interface;

public interface IDeviceTypeService
{
    Task<IPagedList<DeviceType>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy);
    Task<DeviceType> FindAsync(int id);
    Task CreateAsync(CreateDeviceTypeRequest request);
    Task UpdateAsync(int id, CreateDeviceTypeRequest request);
    Task DeleteAsync(int id);
}