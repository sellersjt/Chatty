using Chatty.Api.Entities;
using Chatty.Api.Models.Rooms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatty.Api.Services
{
    public interface IChatService
    {
        IEnumerable<RoomListModel> GetRoomList();
        Task<RoomChatModel> GetRoomChat(int id);
        void CreateRoom(RoomCreateModel model);
    }
}
