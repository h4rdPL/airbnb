using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Tests.Fixtures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace airbnb.Tests.Systems.Services
{
    public class TestCookieService
    {
        [Fact]
        public async Task Login_WithValidCredentials_SetsUserCookieAndReturnsUser()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCookieService = new Mock<ICookieService>();
            var mockPasswordService = new Mock<IPasswordHasherService>();
            var userService = new UsersService(mockUserRepository.Object, mockCookieService.Object, mockPasswordService.Object);

            var user = UserFixture.CreateTestUser();

            mockUserRepository
                .Setup(repo => repo.GetUserByEmail(user.Email))
                .ReturnsAsync(user);

            mockCookieService
                .Setup(cookie => cookie.SetUserCookie(user));

            // Act 
            var result = await userService.Login(user.Email, user.Password);

            // Assert
            Assert.Equal(user, result);

            mockUserRepository.Verify(repo => repo.GetUserByEmail(user.Email), Times.Once);
        }

        [Fact]
        public async Task WhenUserLogout_RemoveCookie()
        {
            // Arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            // Set up the mock to return a valid HttpContext
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = new ServiceCollection()
                .AddScoped<IAuthenticationService>(_ => authenticationServiceMock.Object)
                .BuildServiceProvider();

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var cookieService = new CookieService(httpContextAccessorMock.Object);

            // Act
            await cookieService.OnGetAsync();

            // Assert
            authenticationServiceMock.Verify(x => x.SignOutAsync(
                httpContext,
                CookieAuthenticationDefaults.AuthenticationScheme,
                It.IsAny<AuthenticationProperties>()
            ), Times.Once);
        }

    }
}
