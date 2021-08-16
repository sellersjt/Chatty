using AutoMapper;
using Chatty.Api.Entities;
using Chatty.Api.Models.Rooms;
using Chatty.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatty.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponseModel
            CreateMap<User, AuthenticateResponseModel>();

            // RegisterRequestModel -> User
            CreateMap<RegisterRequestModel, User>();

            // UpdateRequestModel -> User
            CreateMap<UpdateRequestModel, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

            // Room -> RoomListModel
            CreateMap<Room, RoomListModel>();

            // Room -> RoomChatMedel
            CreateMap<Room, RoomChatModel>();

            // RoomCreateModel => Room
            CreateMap<RoomCreateModel, Room>();
        }
    }
}
