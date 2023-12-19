using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUsersService _userService;
        private readonly IEmailService _emailService;

        public UsersController(IUsersService usersService, IEmailService emailService)
        {
            _userService = usersService;
            _emailService = emailService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest">The request to register a new user.</param>
        /// <returns>The result of the registration process.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the user registration process.</exception>
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(AuthenticationRequest registerRequest)
        {
            try
            {
                var user = await _userService.Register(registerRequest);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Password and repeated password do not match.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while registering user", ex);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify(string email, string subject, string message)
        {
            await _emailService.SendEmailAsync(email, subject, message);
            return Ok("Email sent successfully");
        }


        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginRequest">The request to log in a user.</param>
        /// <returns>The authentication response if login is successful.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the login process.</exception>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.Login(loginRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        /// <summary>
        /// Creates a new comment.
        /// </summary>
        /// <param name="postNewComment">The request to create a new comment.</param>
        /// <returns>The result of the comment creation process.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the comment creation process.</exception>
        [HttpPost("comment")]
        public async Task<ActionResult<CreateCommentResponse>> CreateComment(CreateCommentsRequest postNewComment)
        {
            try
            {
                var result = await _userService.CreateComment(postNewComment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while invoking the service", ex);
            }
        }

        /// <summary>
        /// Deletes a user by email.
        /// </summary>
        /// <param name="email">The email of the user to be deleted.</param>
        /// <returns>True if the user was successfully deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the user deletion process.</exception>
        [HttpDelete("removeUser"), Authorize]
        public async Task<ActionResult<bool>> DeleteUser(string email)
        {
            try
            {
                var result = await _userService.DeleteUser(email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while invoking the service", ex);
            }
        }
        }
    }
