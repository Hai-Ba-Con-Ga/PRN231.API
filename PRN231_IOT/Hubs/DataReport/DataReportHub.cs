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
            var (clientId, serialId) = GetInfoClient();
            await Groups.AddToGroupAsync(clientId, serialId);
            await Console.Out.WriteLineAsync($"ClientId:{clientId}, serialId:{serialId} connected");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)        
        {
            var (clientId, serialId) = GetInfoClient();
            await Groups.RemoveFromGroupAsync(clientId, serialId);
            await Console.Out.WriteLineAsync($"ClientId:{clientId}, serialId:{serialId} disconnected");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// First item is clientId, 
        /// Second item is serialId
        /// </returns>
        private (string, string) GetInfoClient()
        {
            var clientId = Context.ConnectionId;
            var serialId = (string?)Context.GetHttpContext()?.Request?.Query["searialId"] ?? "";

            return (clientId, serialId);
        }
    }
}
