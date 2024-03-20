using BusinessObject.Common;
using BusinessObject.Dto.DeviceType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceTypeController : ControllerBase
    {
        private readonly IDeviceTypeService _deviceTypeService;

        public DeviceTypeController(IDeviceTypeService deviceTypeService)
        {
            _deviceTypeService = deviceTypeService;
        }

        /// <summary>
        /// Search device type
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchDeviceType([FromQuery] SearchBaseReq searchRequest)
        {
            var response = await _deviceTypeService.SearchDeviceType(searchRequest);
            return Ok(response);
        }

        /// <summary>
        /// Create device type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDeviceType([FromBody] CreateDeviceTypeRequest request)
        {
            var response = await _deviceTypeService.CreateDeviceType(request);
            return Ok(response);
        }
    }
}
