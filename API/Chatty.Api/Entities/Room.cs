using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Api.Entities
{
    public class Room
    {
        public Room()
        {
            Messages = new List<Message>();
            Users = new List<RoomUser>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<RoomUser> Users { get; set; }
    }
}
