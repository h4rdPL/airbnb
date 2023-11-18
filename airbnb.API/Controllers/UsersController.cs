using airbnb.Application.Common.Interfaces;
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
        public async Task<IActionResult> Register(string Email, string Password, string RepeatedPassword, string FirstName, string LastName)
        {
            var user = await _userService.Register(Email, Password, RepeatedPassword, FirstName, LastName);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Password and repeated password do not match.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            try
            {
                var response = await _userService.Login(Email, Password);
                return Ok(response);
            } catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }

        }
    }
}
