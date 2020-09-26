using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly IHubContext<ChatHub> hubContext;

        public SignalRController(IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPost]
        public async Task Broadcast([FromBody] Envelope env)
        {
            if (env != null)
            {
                await hubContext.Clients.All.SendAsync("Receive", env.User, env.Message);
            }
        }
    }

    public class Envelope
    {
        public string User { get; set; }

        public string Message { get; set; }
    }
}
