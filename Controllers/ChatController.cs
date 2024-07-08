using ChatApplication.Hubs.HubsInterfaces;
using ChatApplication.Hubs;
using ChatApplication.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;
        
        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        [HttpPost]
        [Route("CreateChat")]
        public async Task<IActionResult> CreateChat()
        {
            if (HttpContext.Request.Query.ContainsKey("id") && HttpContext.Request.Query.ContainsKey("userId"))
            {
                int id = int.Parse(HttpContext.Request.Query["id"].ToString());
                int userId = int.Parse(HttpContext.Request.Query["userId"].ToString());

                chatService.Create(id, userId);
            }

            return Ok();
        }
    }
}
