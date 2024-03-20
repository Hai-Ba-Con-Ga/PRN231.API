using BusinessObject.Common;
using BusinessObject.Dto.DeviceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IDeviceTypeService
    {
        Task<PagingApiResponse<DeviceTypeResponse>> SearchDeviceType(SearchBaseReq searchRequest);
        Task<ApiResponse<bool>> CreateDeviceType(CreateDeviceTypeRequest request);

    }
}
