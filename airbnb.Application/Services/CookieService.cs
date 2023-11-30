﻿using airbnb.Application.Common.Interfaces;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(
                _httpContextAccessor.HttpContext,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                    ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                    IsPersistent = true,
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                     CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while returning cookie", ex);
            }
        }
    }
}
