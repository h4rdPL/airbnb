using airbnb.Application.Common.Interfaces;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(string email, string password, string FirstName, string LastName)
        {
            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = email,
                Password = password
            };

            await _userRepository.AddUser(newUser);

            return newUser;
        }
    }

}
