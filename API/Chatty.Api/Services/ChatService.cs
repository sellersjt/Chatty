using AutoMapper;
using Chatty.Api.Authorization;
using Chatty.Api.Entities;
using Chatty.Api.Helpers;
using Chatty.Api.Models.Rooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Api.Services
{
    public class ChatService : IChatService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        public ChatService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public IEnumerable<RoomListModel> GetRoomList()
        {
            List<Room> roomList = _context.Rooms.ToList();

            IEnumerable<RoomListModel> output = _mapper.Map<IEnumerable<RoomListModel>>(roomList);

            return output;
        }

        public async Task<RoomChatModel> GetRoomChat(int id)
        {
            var roomChat = await _context.Rooms.Include(x => x.Messages).ThenInclude(u => u.User).FirstOrDefaultAsync(x => x.Id == id);

            if (roomChat == null)
            {
                throw new KeyNotFoundException("Room not found"); ;
            }

            var output = _mapper.Map<RoomChatModel>(roomChat);

            return output;
        }

        public void CreateRoom(RoomCreateModel model)
        {
            // validate
            if (_context.Rooms.Any(x => x.Name == model.Name))
                throw new AppException("Room '" + model.Name + "' is already taken");

            var room = _mapper.Map<Room>(model);

            _context.Rooms.Add(room);
            _context.SaveChanges();
        }
    }
}
