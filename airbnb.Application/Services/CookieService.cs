using airbnb.Application.Common.Interfaces;
using airbnb.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace airbnb.Application.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Signs out the user asynchronously.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task OnGetAsync()
        {
            // Sign out the user and clear the authentication cookie.
            await AuthenticationHttpContextExtensions.SignOutAsync(
                _httpContextAccessor.HttpContext,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
        }

        /// <summary>
        /// Sets the user authentication cookie.
        /// </summary>
        /// <param name="user">The user for whom the cookie is set.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown when an error occurs during the cookie setting process.</exception>
        public async Task SetUserCookie(User user)
        {
            try
            {
                // Define claims for the user.
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

                // Create a claims identity and set authentication properties.
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                    IsPersistent = true,
                };

                // Sign in the user with the claims identity and authentication properties.
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            catch (Exception ex)
            {
                // Handle exceptions during cookie setting.
                throw new Exception("Error while setting user cookie", ex);
            }
        }
    }
}
