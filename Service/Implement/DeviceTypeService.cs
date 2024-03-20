using BusinessObject.Common.PagedList;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement;

public class DeviceTypeService : BaseService, IDeviceTypeService
{
    public DeviceTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    
    public async Task<IPagedList<DeviceType>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return await _unitOfWork.Resolve<DeviceType>().SearchAsync(keySearch, pagingQuery, orderBy);
    }
    
    public async Task<DeviceType> FindAsync(int id)
    {
        return await _unitOfWork.Resolve<DeviceType>().FindAsync(id);
    }
    
    public async Task CreateAsync(CreateDeviceTypeRequest request)
    {
        try
        {
            var entity = new DeviceType
            {
                TypeName = request.TypeName
            };
            await _unitOfWork.Resolve<DeviceType>().CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Create DeviceType failed", ex);
        }
    }
    
    public async Task UpdateAsync(int id, CreateDeviceTypeRequest request)
    {
        try
        {
            var entity = await _unitOfWork.Resolve<DeviceType>().FindAsync(id);
            if (entity == null)
                throw new Exception("DeviceType not found");
            entity.TypeName = request.TypeName;
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Update DeviceType failed", ex);
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        try
        {
            var entity = await _unitOfWork.Resolve<DeviceType>().FindAsync(id);
            if (entity == null)
                throw new Exception("DeviceType not found");
            _unitOfWork.Resolve<DeviceType>().DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Delete DeviceType failed", ex);
        }
    }
}