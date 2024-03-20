using BusinessObject.Common;
using BusinessObject.Dto.Device;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;
using Mapster;
using Repository.Common;
using Repository.Interface;
using Service.Common;
using Service.Interface;

namespace Service.Implement
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<PagingApiResponse<DeviceResponse>> SearchDevice(SearchBaseReq searchReq)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Device>()
                .SearchAsync<DeviceResponse>(searchReq.KeySearch, searchReq.PagingQuery, searchReq.OrderBy);
                return Success(result);
            }
            catch (Exception ex)
            {
                return PagingFailed<DeviceResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<PagingApiResponse<DeviceResponse>> SearchDeviceByOwnerID(int ownerID, SearchBaseReq searchReq)
        {
            try
            {
                var result = await _unitOfWork.Resolve<Device, IDeviceRepository>()
                .SearchAsync<DeviceResponse>(searchReq.KeySearch, searchReq.PagingQuery, 
                                                searchReq.OrderBy,ownerID);
                return Success(result);
            }
            catch (Exception ex)
            {
                return PagingFailed<DeviceResponse>(ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> CreateDevice(CreateDeviceRequest request)
        {
            try
            {
                var isOwnerExist = await _unitOfWork.Resolve<Account>().IsExist(request.OwnerId);

                if(!isOwnerExist)
                    return Failed<bool>("Owner is not found", System.Net.HttpStatusCode.NotFound);

                var isDeviceTypeExist = await _unitOfWork.Resolve<DeviceType>().IsExist(request.DeviceTypeId);
                if(!isDeviceTypeExist)
                    return Failed<bool>("Device type is not found", System.Net.HttpStatusCode.NotFound);

                var device = request.Adapt<Device>();

                await _unitOfWork.Resolve<Device>().CreateAsync(device);

                var result = await _unitOfWork.SaveChangesAsync();

                return Success(result > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.GetExceptionMessage());
            }
        }
    }
}
