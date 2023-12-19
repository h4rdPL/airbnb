using airbnb.Application.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace airbnb.Tests.Systems.Services
{
    public class EmailServiceTest
    {
        [Theory]
        [InlineData("test@gmail.com")]
        [InlineData("test@outlook.com")]
        public async Task WhenUserEmailIsValid_ShouldReturnTrue(string email)
        {
            // Arrange
            var emailService = new EmailService(Mock.Of<IConfiguration>());

            // Act
            var isValid = await emailService.IsValidEmail(email);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("test.gmail.com")]
        [InlineData("test/outlook.com")]
        [InlineData("test,outlook.com")]
        public async Task WhenUserEmailIsInValid_ShouldReturnFalse(string email)
        {
            // Arrange
            var emailService = new EmailService(Mock.Of<IConfiguration>());

            // Act
            var isValid = await emailService.IsValidEmail(email);

            // Assert
            Assert.False(isValid);
        }
    }
}
