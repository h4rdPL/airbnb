using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
namespace airbnb.Tests.Systems.Services
{
    public class TestUsersService
    {
        [Theory]
        [InlineData("string", "string")]
        [InlineData("Another@Password456", "Another@Password456")]
        public async Task When_PasswordAndRepeatedPasswordMatch_ReturnsTrue(string Password, string RepeatedPassword)
        {
            // Arrange
            var mockService = new Mock<IUsersService>();
            var mockRepository = new Mock<IUserRepository>();
            var mockCookieService = new Mock<ICookieService>();
            var mockPasswordService = new Mock<IPasswordHasherService>();
            var registerRequest = new AuthenticationRequest("John", "Doe", "john.doe@gmail.com", Password, RepeatedPassword);
            var authResponse = new AuthResponse("John", "Doe", "john.deo@gmail.com");
            
            mockService
                .Setup(service => service.Register(registerRequest))
                .ReturnsAsync(authResponse);

            var userService = new UsersService(mockRepository.Object, mockCookieService.Object, mockPasswordService.Object);

            // Act
            var result = await userService.Register(registerRequest);

            // Assert
            if (Password == RepeatedPassword)
            {
                Assert.NotNull(result);
                mockRepository.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
            }
        }
    }

}
