using Moq;
using airbnb.Application.Common.Interfaces;
using airbnb.Tests.Fixtures;
using airbnb.Domain.Models;
using airbnb.Application.Services;

namespace airbnb.Tests.Systems.Services
{
    public class TestUsersService
    {
        [Theory]
        [InlineData("test", "test")]
        [InlineData("Another@Password456", "Another@Password456")]
        public async Task When_PasswordAndRepeatedPasswordMatch_ReturnsTrue(string Password, string RepeatedPassword)
        {
            // Arrange
            var mockService = new Mock<IUsersService>();
            var mockRepository = new Mock<IUserRepository>();
            mockService
                .Setup(service => service.Register(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(UserFixture.CreateTestUser());

            var userService = new UsersService(mockRepository.Object);

            // Act
            var result = await userService.Register("john.doe@gmail.com", Password, RepeatedPassword, "John", "Doe");

            // Assert
            if (Password == RepeatedPassword)
            {
                Assert.NotNull(result);
                mockRepository.Verify(repo => repo.AddUser(It.IsAny<User>()), Times.Once);
            }
        }
    }

}
