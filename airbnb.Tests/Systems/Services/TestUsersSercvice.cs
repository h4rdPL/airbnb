using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Moq;

namespace airbnb.Tests.Systems.Services
{
    public class TestUsersService
    {
        [Theory]
        [InlineData("john.doe@gmail.com", "string", "John", "Doe")]
        public async Task Register_ValidUser_ReturnsRegisteredUser(string Email, string Password, string FirstName, string LastName)
        {
            // Arrange
            var mockService = new Mock<IUsersService>();
            mockService
                .Setup(service => service.Register(Email, Password, FirstName, LastName))
                .ReturnsAsync(UserFixture.CreateTestUser());

            var userService = mockService.Object; 

            // Act
            var result = await userService.Register(Email, Password, FirstName, LastName);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.FirstName.Should().Be(FirstName);
            result.LastName.Should().Be(LastName);
            result.Email.Should().Be(Email);
            result.Password.Should().Be(Password);
        }

     


        [Theory]
        [InlineData("john.doe@gmail.com", "string")]
        public async Task Login_ValidUser_ReturnsLoggedInUser(string Email, string Password)
        {
            // Arrange
            var mockService = new Mock<IUsersService>();
            var expectedUser = UserFixture.CreateTestUser();

            mockService
                .Setup(service => service.Login(Email, Password))
                .ReturnsAsync(expectedUser);

            var userService = mockService.Object;

            // Act
            var result = await userService.Login(Email, Password);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<User>();
            result.Email.Should().Be(Email);
            result.Password.Should().Be(Password);
        }

        [Theory]
        [InlineData("john.doe@gmail.com", "string")]
        public async Task GetUserByEmail_UserExists_ReturnsUser(string email, string userPassword)
        {
            // Arrange
            var mockService = new Mock<IUsersService>();
            var expectedUser = UserFixture.CreateTestUser();
            var hasher = new PasswordHasherService();

            // Set up the mock to return the expected user
            mockService
                .Setup(service => service.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(expectedUser);

            var userService = mockService.Object;

            // Act
            var result = await userService.GetUserByEmail(email);

            // Assert
            result.Should().BeOfType<User>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedUser);

            // Sprawdzenie hasła
            var passwordIsCorrect = hasher.VerifyPassword(hasher.HashPassword(expectedUser.Password), userPassword);
            passwordIsCorrect.Should().BeTrue();
        }


    }

}
