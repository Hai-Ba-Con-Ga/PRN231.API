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
    public async Task<PagingApiResponse<DatumResponse>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        try 
        {
            var result = await _unitOfWork.Resolve<CollectedDatum>()
                .SearchAsync<DatumResponse>(keySearch, pagingQuery, orderBy);

            return Success(result);
        }
        catch (Exception ex)
        {
            return PagingFailed<DatumResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<DatumResponse>> GetCollectedData(int id)
    {
        try 
        {
            var result = await _unitOfWork.Resolve<CollectedDatum>().FindAsync(id);

            if (result == null)
                return Failed<DatumResponse>("Datum is not found");

            return Success(result.Adapt<DatumResponse>());
        }
        catch (Exception ex)
        {
            return Failed<DatumResponse>(ex.GetExceptionMessage());
        }
    }

    public async Task<ApiResponse<bool>> CreateAsync(CollectedDataRequest request)
    {
        try
        {
            var entity = request.Adapt <CollectedDatum>();
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