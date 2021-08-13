using Chatty.Api.Authorization;
using Chatty.Api.Hubs;
using Chatty.Api.Hubs.Clients;
using Chatty.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chathub;

        public ChatController(IHubContext<ChatHub, IChatClient> chathub)
        {
            _chathub = chathub;
        }

        [HttpPost("messages")]
        public async Task Post(ChatMessage message)
        {
            // save to database

            await _chathub.Clients.All.ReceiveMessage(message);
        }
    }

}
