using airbnb.Application.Common.Interfaces;
using airbnb.Infrastructure.Persistence;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace airbnb.Tests.Systems.Repository
{
    public class TestUserRepository
    {

        [Fact]
        public async Task AddUserToRepository_ShouldAddUserToDatabase()
        {
            // Arrange
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockEmailService = new Mock<IEmailService>();

            using var dbContext = new AirbnbDatabaseFake().Context;
            var mockPasswordHasher = new Mock<IPasswordHasherService>();
            var mockRepository = new UserRepository(dbContext, httpContextAccessor.Object, mockEmailService.Object);

            var testUser = UserFixture.CreateTestUser();

            // Act
            await mockRepository.AddUser(testUser);

            // Assert
            var savedUser = await dbContext.Users.FindAsync(testUser.Id);
            savedUser.Should().NotBeNull();
            savedUser.FirstName.Should().Be(testUser.FirstName);
            savedUser.LastName.Should().Be(testUser.LastName);
            savedUser.Email.Should().Be(testUser.Email);
            savedUser.Password.Should().Be(testUser.Password);
        }
    }
}
