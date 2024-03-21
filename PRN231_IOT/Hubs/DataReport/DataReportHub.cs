using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace WebAPI.Hubs.DataReport
{
    /// <summary>
    /// Hub for realtime data report from devices
    /// </summary>
    public class DataReportHub : Hub
    {
        /// <summary>
        /// _connections : Key = SerialId, Value = ConnectionId
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> _connections = new();

        public DataReportHub()
        {
        }

        public string? GetConnectionId(string serialId)
        {
            _connections.TryGetValue(serialId, out string? connectionId);
            return connectionId;
        }

        public override Task OnConnectedAsync()
        {
            var clientId = Context.ConnectionId;
            var serialId = (string)Context.GetHttpContext().Request.Query["searialId"];

            _connections.TryAdd(serialId, clientId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var clientId = Context.ConnectionId;
            var serialId = (string)Context.GetHttpContext().Request.Query["searialId"];

            _connections.Remove(serialId ?? "", out _);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
