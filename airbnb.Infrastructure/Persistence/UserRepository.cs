using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace airbnb.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AirbnbDbContext _context;
        public UserRepository(AirbnbDbContext context)
        {
            _context = context;
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

        public Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }
    }
}
