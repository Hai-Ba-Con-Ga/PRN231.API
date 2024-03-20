using BusinessObject.Common;
using BusinessObject.Dto.DeviceType;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers;

[Route("api/v1/device-type")]
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
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq request)
    {
        var result = await _deviceTypeService.SearchAsync(request.KeySearch, request.PagingQuery, request.OrderBy);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceType(int id)
    {
        var result = await _deviceTypeService.GetDeviceType(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceTypeRequest request)
    {
        var result = await _deviceTypeService.CreateAsync(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDeviceTypeRequest request)
    {
        var result = await _deviceTypeService.UpdateAsync(id, request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deviceTypeService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }


}