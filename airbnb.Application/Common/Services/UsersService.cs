using airbnb.Application.Common.Interfaces;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Services
{
    public class UsersService : IUsersService
    {
        public Task<User> Register(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
