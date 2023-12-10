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
        /// Creates a new comment based on the provided comment request.
        /// </summary>
        /// <param name="postNewComment">The request for creating a new comment.</param>
        /// <returns>The response containing information about the created comment.</returns>
        public async Task<CreateCommentResponse> CreateComment(CreateCommentsRequest postNewComment)
        {
            var response = await _userRepository.CreateComment(postNewComment);
            var mappedResponse = _mapper.Map<CreateCommentResponse>(response);
            return mappedResponse;
        }

        /// <summary>
        /// Deletes a user based on the provided user email.
        /// </summary>
        /// <param name="userEmail">The email of the user to be deleted.</param>
        /// <returns>True if the user is successfully deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs while invoking the repository.</exception>
        public async Task<bool> DeleteUser(string userEmail)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(userEmail);
                if (user is null)
                {
                    throw new Exception("User does not exist");
                }
                var result = await _userRepository.DeleteUser(user);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while invoking repository", ex);
            }
        }

        /// <summary>
        /// Retrieves a user based on the provided email.
        /// </summary>
        /// <param name="email">The email of the user to be retrieved.</param>
        /// <returns>The user information.</returns>
        public async Task<User> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            return result;
        }

        /// <summary>
        /// Logs in a user based on the provided login request.
        /// </summary>
        /// <param name="loginRequest">The request containing user login information.</param>
        /// <returns>The authentication response for the logged-in user.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the login process.</exception>
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
        /// Registers a new user based on the provided authentication request.
        /// </summary>
        /// <param name="authenticationRegister">The request containing user registration information.</param>
        /// <returns>The authentication response for the registered user.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the registration process.</exception>
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
                throw new Exception("Passwords do not match");
            }

            await _userRepository.AddUser(newUser);
            return new AuthResponse(newUser.FirstName, newUser.LastName, newUser.Email);
        }
    }

}
