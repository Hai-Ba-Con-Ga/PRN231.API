using BusinessObject.Common;
using BusinessObject.Common.PagedList;
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
    
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq request)
    {
        var result = await _deviceTypeService.SearchAsync(request.KeySearch, request.PagingQuery, request.OrderBy);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Find(int id)
    {
        var result = await _deviceTypeService.FindAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceTypeRequest request)
    {
        await _deviceTypeService.CreateAsync(request);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateDeviceTypeRequest request)
    {
        await _deviceTypeService.UpdateAsync(id, request);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deviceTypeService.DeleteAsync(id);
        return Ok();
    }
    
    
}