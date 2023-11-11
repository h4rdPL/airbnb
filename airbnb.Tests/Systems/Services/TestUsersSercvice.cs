using airbnb.Application.Common.Interfaces;
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
    }

}
