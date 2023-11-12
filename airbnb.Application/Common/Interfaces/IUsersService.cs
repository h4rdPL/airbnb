using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> Login(string email, string password);
        public Task<User> Register(string email, string password, string FirstName, string LastName);
    }
}
