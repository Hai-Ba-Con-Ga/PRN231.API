using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace WebAPI.Hubs.DataReport
{
    /// <summary>
    /// Hub for realtime data report from devices
    /// </summary>
    public class DataReportHub : Hub
    {
        public DataReportHub()
        {
        }

        public override async Task OnConnectedAsync()
        {
            var clientId = Context.ConnectionId;
            var serialId = (string)Context.GetHttpContext().Request.Query["searialId"];

            await Groups.AddToGroupAsync(clientId, serialId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var clientId = Context.ConnectionId;
            var serialId = (string)Context.GetHttpContext().Request.Query["searialId"];

            await Groups.RemoveFromGroupAsync(clientId, serialId);
        }
    }
}
