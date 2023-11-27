using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _userService;

        public UsersController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(AuthenticationRequest RegisterRequest)
        {
            try
            {
                var user = await _userService.Register(RegisterRequest);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Password and repeated password do not match.");
                }
            } catch (Exception ex)
            {
                throw new Exception("Error while register user", ex);
            }
           
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.Login(loginRequest);
                return Ok(response);
            } catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            
        }

        public async Task<ActionResult<CreateCommentResponse>> CreateComment(CreateCommentsRequest postNewComment)
        {
            try
            {
                var result = await _userService.CreateComment(postNewComment);
                return Ok(result);
            } catch (Exception ex)
            {
                throw new Exception("En error occured while invoke service", ex);
            }
        }
    }
}
