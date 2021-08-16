using Chatty.Api.Entities;

namespace Chatty.Api.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public int? ValidateToken(string token);
    }
}
