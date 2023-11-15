using airbnb.Application.Common.Interfaces;
using airbnb.Domain.Models;

namespace airbnb.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;

        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            return result;
        }

        public Task<User> Login(string email, string password)
        {
            throw new NotImplementedException();
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
