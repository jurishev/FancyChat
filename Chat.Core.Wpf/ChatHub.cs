using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Core.Wpf
{
    internal class ChatHub : Hub
    {
        public async Task Broadcast(string user, string msg)
        {
            await Clients.All.SendAsync("Receive", user, msg);
        }
    }
}
