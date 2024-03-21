using BusinessObject.Common;
using BusinessObject.Dto.Device;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers
{
    [Route("api/v1/device")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        /// <summary>
        /// search device
        /// </summary>
        /// <param name="searchReq"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SearchDevice([FromQuery] SearchBaseReq searchReq)
        {
            var result = await _deviceService.SearchDevice(searchReq);
            return Ok(result);
        }

        /// <summary>
        /// Search device by owner id
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="searchReq"></param>
        /// <returns></returns>
        [HttpGet("owner/{ownerID}")]
        public async Task<IActionResult> SearchDeviceByOwnerID(int ownerID, [FromQuery] SearchBaseReq searchReq)
        {
            var result = await _deviceService.SearchDeviceByOwnerID(ownerID, searchReq);
            return Ok(result);
        }

        /// <summary>
        /// Create new device
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceRequest request)
        {
            var result = await _deviceService.CreateDevice(request);
            return Ok(result);
        }
    }
}
