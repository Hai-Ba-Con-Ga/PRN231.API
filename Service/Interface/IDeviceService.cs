using BusinessObject.Common;
using BusinessObject.Dto.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDeviceService
    {
        Task<PagingApiResponse<DeviceResponse>> SearchDevice(SearchBaseReq searchReq);
        Task<PagingApiResponse<DeviceResponse>> SearchDeviceByOwnerID(int ownerID, SearchBaseReq searchReq);
        Task<ApiResponse<bool>> CreateDevice(CreateDeviceRequest request);
    }
}
