using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using BusinessObject.Dto.CollectData;
using BusinessObject.Model;
using Mapster;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement;

public class CollectedDataService : BaseService, ICollectedDataService
{
    public CollectedDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
    public async Task<PagingApiResponse<DataResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        try 
        {
            var result = await _unitOfWork.Resolve<CollectedDatum>()
                .SearchAsync<DataResponse>(keySearch, pagingQuery, orderBy);

        return Success(result);
        }
        catch (Exception ex)
        {
            return PagingFailed<DataResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<DataResponse>> GetCollectedData(int id)
    {
        try 
        {
            var result = await _unitOfWork.Resolve<CollectedDatum>().FindAsync(id);

            if (result == null)
                return Failed<DataResponse>("Datum is not found");

            return Success(result.Adapt<DataResponse>());
        }
        catch (Exception ex)
        {
            return Failed<DataResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> CreateAsync(CollectedDataRequest request)
    {
        try
        {
            var entity = request.Adapt <CollectedDatum>();

            var device = await _unitOfWork.Resolve<Device>().FindAsync(x => x.SerialId == request.SerialId);

            if (device == null)
                return Failed<bool>("Device is not found");

            entity.DeviceId = device.DeviceId;

            await _unitOfWork.Resolve<CollectedDatum>().CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return Success(true);
        }catch(Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }

    public  async Task<ApiResponse<bool>> UpdateAsync(int id, CollectedDataRequest request)
    {
        try 
        {
            var entity = await _unitOfWork.Resolve<CollectedDatum>().FindAsync(id);

            if (entity == null)
                return Failed<bool>("Datum is not found");

            entity = request.Adapt(entity);
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
            var entity = await _unitOfWork.Resolve<CollectedDatum>().FindAsync(id);

            if (entity == null)
                return Failed<bool>("Datum is not found");

            _unitOfWork.Resolve<CollectedDatum>().DeleteAsync(entity);
            _unitOfWork.SaveChangesAsync();

            return Success(true);
        }
        catch (Exception ex)
        {
            return Failed<bool>(ex.GetExceptionMessage());
        }
    }
}