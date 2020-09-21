using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Angular
{
    /// <summary>
    /// SignalR public hub.
    /// </summary>
    internal class ChatHub : Hub
    {
        public async Task Broadcast(string user, string msg)
        {
            await Clients.All.SendAsync("Receive", user, msg);
        }
    }
}
