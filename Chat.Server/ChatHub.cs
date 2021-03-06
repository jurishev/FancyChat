﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Server
{
    /// <summary>
    /// SignalR public hub.
    /// </summary>
    public class ChatHub : Hub
    {
        public async Task Broadcast(string user, string msg)
        {
            await Clients.All.SendAsync("Receive", user, msg);
        }
    }
}
