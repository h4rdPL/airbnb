using airbnb.Application.Common.Services;

namespace airbnb.Tests.Systems.Services
{
    public class TestHashPasswordService
    {
        [Theory]
        [InlineData("string")]
        public void HashPassword_ShouldReturn_HashedPassword(string password)
        {
            // Arrange
            var hasher = new PasswordHasherService();

            // Act
            var hashedPassword = hasher.HashPassword(password);

            // Assert
            Assert.NotNull(hashedPassword);
            Assert.NotEqual(password, hashedPassword);
            Assert.Contains(Convert.ToBase64String(hasher.Salt), hashedPassword);
            Assert.Contains(Convert.ToBase64String(hasher.Hash), hashedPassword);
        }

    }
}
