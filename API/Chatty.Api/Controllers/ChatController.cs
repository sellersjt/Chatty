using AutoMapper;
using Chatty.Api.Authorization;
using Chatty.Api.Entities;
using Chatty.Api.Helpers;
using Chatty.Api.Hubs;
using Chatty.Api.Hubs.Clients;
using Chatty.Api.Models;
using Chatty.Api.Models.Rooms;
using Chatty.Api.Services;
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
        private readonly DataContext _context;
        private IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatController(
            IHubContext<ChatHub, 
            IChatClient> chathub,
            DataContext context,
            IMapper mapper,
            IChatService chatService)
        {
            _chathub = chathub;
            _context = context;
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpGet("getRoomList")]
        public IActionResult GetRooms()
        {
            var rooms = _chatService.GetRoomList();

            return Ok(rooms);
        }

        [HttpGet("getRoomChat/{id}")]
        public IActionResult GetRoomChat(int id)
        {
            var roomChat = _chatService.GetRoomChat(id);

            return Ok(roomChat.Result);
        }

        [HttpPost("createRoom")]
        public IActionResult CreateRoom(RoomCreateModel model)
        {
            _chatService.CreateRoom(model);

            return Ok(new { message = "Room Created successful" });
        }





        [HttpPost("sendMessage")]
        public async Task Post(ChatMessage message)
        {
            // save to database
            Message newMessage = new Message
            {
                RoomId = 1,
                UserId = 1,
                Text = message.Message
            };
            _context.Messages.Add(newMessage);
            await _context.SaveChangesAsync();


            await _chathub.Clients.All.ReceiveMessage(message);
        }
    }

}
