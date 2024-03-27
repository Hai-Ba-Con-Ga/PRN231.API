using BusinessObject.Dto.ReportData;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace WebAPI.Hubs.DataReport
{
    public static class DataReportHubExtensionMethods
    {
        /// <summary>
        /// Extension method to send data report to client
        /// </summary>
        /// <param name="serialId"></param>
        /// <param name="hubContext"></param>
        /// <param name="dataReportResponse"></param>
        /// <returns></returns>
        public static async Task SendDataReport(this IHubContext<DataReportHub>? hubContext,
            string serialId, DataReportResponse dataReportResponse)
        {
            if (hubContext == null)
            {
                await Task.CompletedTask.ConfigureAwait(false);
                return;
            }

            var objJsonString = JsonSerializer.Serialize(dataReportResponse);

            //await hubContext.Clients.Group(serialId).SendAsync("ReceiveDataReport", objJsonString);
            await hubContext.Clients.Group(serialId).SendAsync("ReceiveDataReport", dataReportResponse);
        }
    }
}
