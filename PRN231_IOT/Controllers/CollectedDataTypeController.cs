using BusinessObject.Common;
using BusinessObject.Dto.CollectData;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v1/data-type")]
public class CollectedDataTypeController : ControllerBase
{
    private readonly IDataTypeService _dataTypeService;

    public CollectedDataTypeController(IDataTypeService dataTypeService)
    {
        _dataTypeService = dataTypeService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq request)
    {
        var result = await _dataTypeService.SearchAsync(request.KeySearch, request.PagingQuery, request.OrderBy);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _dataTypeService.GetDeviceType(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DataTypeRequest request)
    {
        var result = await _dataTypeService.CreateAsync(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DataTypeRequest request)
    {
        var result = await _dataTypeService.UpdateAsync(id, request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _dataTypeService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }
}