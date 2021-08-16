using Chatty.Api.Entities;
using Chatty.Api.Models.Users;
using System.Collections.Generic;

namespace Chatty.Api.Services
{
    public interface IUserService
    {
        AuthenticateResponseModel Authenticate(AuthenticateRequestModel model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequestModel model);
        void Update(int id, UpdateRequestModel model);
        void Delete(int id);
    }
}
