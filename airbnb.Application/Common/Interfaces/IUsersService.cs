using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUserByEmail(string Email);
        Task<User> Login(string Email, string Password);
        public Task<User> Register(string Email, string Password, string RepeatedPassword, string FirstName, string LastName);
    }
}
