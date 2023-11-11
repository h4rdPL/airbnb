using airbnb.Application.Common.Interfaces;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;

namespace airbnb.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AirbnbDbContext _context;
        private readonly IPasswordHasherService _passwordHasherService;
        public UserRepository(AirbnbDbContext context, IPasswordHasherService passwordHasherService)
        {
            _context = context;
            _passwordHasherService = passwordHasherService;

        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
