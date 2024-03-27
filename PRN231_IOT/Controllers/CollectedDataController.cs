using BusinessObject.Common;
using BusinessObject.Dto.CollectData;
using BusinessObject.Dto.ReportData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Interface;
using WebAPI.Hubs.DataReport;

namespace WebAPI.Controllers;

[Route("api/v1/collected-data")]
[ApiController]
public class CollectedDataController : ControllerBase
{
    private readonly ICollectedDataService _dataService;
    private readonly IServiceProvider _serviceProvider;

    public CollectedDataController(ICollectedDataService datumService, IServiceProvider serviceProvider)
    {
        _dataService = datumService;
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchBaseReq request)
    {
        var result = await _dataService.SearchAsync(request.KeySearch, request.PagingQuery, request.OrderBy);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCollectedData(int id)
    {
        var result = await _dataService.GetCollectedData(id);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CollectedDataRequest request)
    {
        var result = await _dataService.CreateAsync(request);

        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _ = Task.Run(async () =>
            {
                var dataReportHubContext = _serviceProvider.GetService<IHubContext<DataReportHub>>();

                if (dataReportHubContext == null)
                    return;
                
                await dataReportHubContext.SendDataReport(request.SerialId, new ReportDataResponse
                {
                    CreatedDate = DateTime.Now,
                    DataValue = request.DataValue,
                    DataUnit = request.DataUnit,
                    TypeId = request.CollectedDataTypeId,
                });

            }).ConfigureAwait(false);
        }

        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CollectedDataRequest request)
    {
        var result = await _dataService.UpdateAsync(id, request);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _dataService.DeleteAsync(id);
        return StatusCode((int)result.StatusCode, result);
    }
}