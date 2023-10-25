using airbnb.Application.Services.Authentication;
using airbnb.Contracts.Authentication;
using airbnb.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// User register method
        /// </summary>
        /// <param name="user">UserDTO data</param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<AuthenticationResponse> Register(UserDTO user)
        {
            var authResult = _authenticationService.Register(user);
            var response = new AuthenticationResponse(authResult.user.Username, authResult.user.Email, authResult.user.Password);
            return response;
        }

        /// <summary>
        /// User login method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("Login")]
        public async Task<AuthenticationResponse> Login(UserLoginDTO user)
        {
            var authResult = _authenticationService.Login(user);
            var response = new AuthenticationResponse(authResult.user.Username, authResult.user.Email, authResult.user.Password);
            return response;
        }
    }


}
