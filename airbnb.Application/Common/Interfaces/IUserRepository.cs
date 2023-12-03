using airbnb.Contracts.Authentication;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment);
        Task<bool> DeleteUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
