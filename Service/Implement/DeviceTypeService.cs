using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;
using Mapster;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement;

public class DeviceTypeService : BaseService, IDeviceTypeService
{
    public DeviceTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagingApiResponse<DeviceTypeResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        try
        {
            var result = await _unitOfWork.Resolve<DeviceType>()
                .SearchAsync<DeviceTypeResponse>(keySearch, pagingQuery, orderBy);

            return Success(result);
        }
        catch (Exception ex)
        {
            return PagingFailed<DeviceTypeResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<DeviceTypeResponse>> GetDeviceType(int id)
    {
        try
        {
            var result = await _unitOfWork.Resolve<DeviceType>().FindAsync(id);

            if (result == null)
                return Failed<DeviceTypeResponse>("Device type is not found");

            return Success(result.Adapt<DeviceTypeResponse>());
        }
        catch (Exception ex)
        {
            return Failed<DeviceTypeResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> CreateAsync(CreateDeviceTypeRequest request)
    {
        try
        {
            var entity = new DeviceType
            {
                TypeName = request.TypeName
            };

            await _unitOfWork.Resolve<DeviceType>().CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> UpdateAsync(int id, CreateDeviceTypeRequest request)
    {
        try
        {
            var entity = await _unitOfWork.Resolve<DeviceType>().FindAsync(id);
            if (entity == null)
                throw new Exception("DeviceType not found");
            entity.TypeName = request.TypeName;
            await _unitOfWork.SaveChangesAsync();

            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        try
        {
            var entity = await _unitOfWork.Resolve<DeviceType>().FindAsync(id);

            if (entity == null)
                throw new Exception("DeviceType not found");

            await _unitOfWork.Resolve<DeviceType>().DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }
}