using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IUsersService
    {
        public Task<User> Register(string email, string password, string FirstName, string LastName);
    }
}
