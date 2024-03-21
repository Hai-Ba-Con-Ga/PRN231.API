using System.ComponentModel.DataAnnotations;
using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.CollectData;
using BusinessObject.Model;
using Mapster;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement;

public class DataTypeService : BaseService, IDataTypeService
{
    public DataTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagingApiResponse<DataTypeResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery,
        string orderBy)
    {
        try
        {
            var result = await _unitOfWork.Resolve<CollectedDataType>()
                .SearchAsync<DataTypeResponse>(keySearch, pagingQuery, orderBy);

            return Success(result);
        }
        catch (Exception ex)
        {
            return PagingFailed<DataTypeResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<DataTypeResponse>> GetDeviceType(int id)
    {
        try
        {
            var result = await _unitOfWork.Resolve<CollectedDataType>().FindAsync(id);

            if (result == null)
                return Failed<DataTypeResponse>("Device type is not found");

            return Success(result.Adapt<DataTypeResponse>());
        }
        catch (Exception ex)
        {
            return Failed<DataTypeResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> CreateAsync(DataTypeRequest request)
    {
        try
        {
            var entity = request.Adapt<CollectedDataType>();

            await _unitOfWork.Resolve<CollectedDataType>().CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> UpdateAsync(int id, DataTypeRequest request)
    {
        try
        {
            var entity = await _unitOfWork.Resolve<CollectedDataType>().FindAsync(id);

            if (entity == null)
                return Failed<bool>("Device type is not found");

            entity.DataTypeName = request.DataTypeName;
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
            var entity = await _unitOfWork.Resolve<CollectedDataType>().FindAsync(id);
            if (entity == null)
                return Failed<bool>("Device type is not found");
            await _unitOfWork.Resolve<CollectedDataType>().DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }
}