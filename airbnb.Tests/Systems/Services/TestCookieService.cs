using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Contracts.Authentication;
using airbnb.Tests.Fixtures;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace airbnb.Tests.Systems.Services
{
    public class TestCookieService
    {
        [Theory]
        [InlineData("test@gmail.com", "password")]
        public async Task Login_WithValidCredentials_SetsUserCookieAndReturnsUser(string email, string password)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockCookieService = new Mock<ICookieService>();
            var mockPasswordService = new Mock<IPasswordHasherService>();
            var mockMapper = new Mock<IMapper>();
            var userService = new UsersService(mockUserRepository.Object, mockCookieService.Object, mockPasswordService.Object, mockMapper.Object);

            var user = UserFixture.CreateTestUser();
            var loginRequest = new LoginRequest(email, password) ; 

            mockUserRepository
                .Setup(repo => repo.GetUserByEmail(loginRequest.Email))
                .ReturnsAsync(user);

            mockCookieService
                .Setup(cookie => cookie.SetUserCookie(user));

            // Act 
            var result = await userService.Login(loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);

            mockUserRepository.Verify(repo => repo.GetUserByEmail(loginRequest.Email), Times.Once);
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
