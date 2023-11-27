using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using airbnb.Domain.Models;

namespace airbnb.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICookieService _cookieService;
        private readonly IPasswordHasherService _passwordHasher;

        public UsersService(IUserRepository userRepository, ICookieService cookieService, IPasswordHasherService passwordHasher)
        {
            _userRepository = userRepository;
            _cookieService = cookieService;
            _passwordHasher = passwordHasher;
        }

        public async Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment)
        {
            var result = await _userRepository.CreateComment(postNewComment);
            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            return result;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(loginRequest.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var result = _cookieService.SetUserCookie(user);
                return new AuthResponse(user.FirstName, user.LastName, user.Email);

            }
            catch (Exception ex)
            {
                throw new Exception("Error while setting user Cookie", ex);
            }
        }

        public async Task<AuthResponse> Register(AuthenticationRequest authenticationRegister)
        {
            var newUser = new User
            {
                FirstName = authenticationRegister.FirstName,
                LastName = authenticationRegister.LastName,
                Email = authenticationRegister.Email,
                Password = authenticationRegister.Password,
            };

            if (authenticationRegister.Password != authenticationRegister.RepeatedPassword)
            {
                throw new Exception("Password are not the same");
        
            }
            await _userRepository.AddUser(newUser);
            return new AuthResponse(newUser.FirstName, newUser.LastName, newUser.Email);
        }


    }

}
