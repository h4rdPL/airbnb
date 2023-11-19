using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUserByEmail(string Email);
        Task<AuthResponse> Login(LoginRequest loginRequest);
        Task<AuthResponse> Register(AuthenticationRequest authenticationRegiter);
    }
}
