using BusinessObject.Common;
using BusinessObject.Dto.CollectData;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers;

[Route("api/v1/datum")]
[ApiController]
public class DatumController : ControllerBase
{
    private readonly IDatumService _datumService;

    public DatumController(IDatumService datumService)
    {
        _datumService = datumService;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq request)
    {
        var result = await _datumService.SearchAsync(request.KeySearch, request.PagingQuery, request.OrderBy);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDatum(int id)
    {
        var result = await _datumService.GetDatum(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DatumRequest request)
    {
        var result = await _datumService.CreateAsync(request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DatumRequest request)
    {
        var result = await _datumService.UpdateAsync(id, request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _datumService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }
}