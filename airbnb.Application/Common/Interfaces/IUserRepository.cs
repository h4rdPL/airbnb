using airbnb.Domain.Models;
namespace airbnb.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user); 
    }
}
