using BusinessObject.Common;
using BusinessObject.Dto.DeviceType;
using BusinessObject.Model;
using Mapster;
using Repository.Common;
using Service.Common;
using Service.Interface;

namespace Service.Implement
{
    public class DeviceTypeService : BaseService, IDeviceTypeService
    {
        public DeviceTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponse<bool>> CreateDeviceType(CreateDeviceTypeRequest request)
        {
            try
            {
                var deviceTypeRepo = _unitOfWork.Resolve<DeviceType>();

                var deviceType = request.Adapt<DeviceType>();

                ArgumentNullException.ThrowIfNull(deviceType, nameof(deviceType));

                await deviceTypeRepo.CreateAsync(deviceType);
                var rowEff = await _unitOfWork.SaveChangesAsync();

                return Success(rowEff > 0);
            }
            catch (Exception ex)
            {
                return Failed<bool>(ex.Message);
            }

        }

        public async Task<PagingApiResponse<DeviceTypeResponse>> SearchDeviceType(SearchBaseReq searchReq)
        {
            try
            {
                var deviceTypes = await _unitOfWork.Resolve<DeviceType>()
                                .SearchAsync<DeviceTypeResponse>(searchReq.KeySearch,searchReq.PagingQuery,searchReq.OrderBy);

                return Success(deviceTypes);
            }
            catch (Exception ex)
            {
                return PagingFailed<DeviceTypeResponse>(ex.Message);
            }
        }
    }
}
