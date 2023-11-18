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

        public async Task OnGetAsync()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(
                _httpContextAccessor.HttpContext,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
        }


        public async Task SetUserCookie(User user)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.ToLocalTime().AddMinutes(10),
                    IsPersistent = true,
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    "default",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            } catch (Exception ex)
            {
                throw new Exception("Error while return cookie", ex);
            }
      
        }
    }
}
