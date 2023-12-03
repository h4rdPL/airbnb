using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace airbnb.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly AirbnbDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public UserRepository(AirbnbDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Method which register user
        /// </summary>
        /// <param name="user">User entity</param>
        /// <returns>User entity</returns>
        /// <exception cref="NotImplementedException">Regex error, incorrect repeated password</exception>
        public async Task AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
       
        /// <summary>
        /// Method which create comment for the room
        /// </summary>
        /// <param name="postNewComment">Create room response</param>
        /// <returns>New comment for individual room's id</returns>
        /// <exception cref="Exception">Data errror</exception>
        public async Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment)
        {
            try
            {
                var myIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                
                if(myIdClaim is null) 
                {
                    throw new Exception("User error");
                }
                var comment = new Comment
                {
                    UserFirstName = postNewComment.UserFirstName,
                    CommentValue = postNewComment.Comment,
                    NumberOfStars = postNewComment.NumberOfStars,
                    CreatedAt = DateTime.UtcNow,
                };

                var result = await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return new CreateCommentResponse(comment.UserFirstName, comment.CommentValue, comment.NumberOfStars, comment.CreatedAt);


            }
            catch (Exception ex)
            {
                throw new Exception("En erroc occured while insert comment to the database");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                if(user is not null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            } catch(Exception ex)
            {
                throw new Exception("An error occured while remove user from the database", ex);
            }
        }

        /// <summary>
        /// Method which search user by email address 
        /// </summary>
        /// <param name="email">User Email Address</param>
        /// <returns>User entity</returns>
        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            return result;
        }
    }
}
