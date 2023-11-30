using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;
using AutoMapper;

namespace airbnb.Application.Services
{
    public class UsersService : IUsersService
    {

        private readonly IUserRepository _userRepository;
        private readonly ICookieService _cookieService;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IMapper _mapper;
        
        public UsersService(IUserRepository userRepository, ICookieService cookieService, IPasswordHasherService passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _cookieService = cookieService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postNewComment"></param>
        /// <returns></returns>
        public async Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment)
        {
            var response = await _userRepository.CreateComment(postNewComment);
            var mappedResponse = _mapper.Map<CreateCommentResponse>(response);
            return mappedResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationRegister"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
