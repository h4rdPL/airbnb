using airbnb.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Register(string Email, string Password, string FirstName, string LastName)
        {
            var user = _userService.Register(Email, Password,FirstName, LastName);
            return Ok(user);
        }
    }
}
