using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using airbnb.Infrastructure.Rooms;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace airbnb.Tests.Systems.Repository
{
    public class TestRoomRepository
    {
        [Fact]
        public async Task CreateOffer_WhenValidOfferProvided_ShouldAddToDatabase()
        {
            // Arrange
            var mockHttpContext = new Mock<IHttpContextAccessor>();

            // Set up the HttpContext for the mock
            mockHttpContext.Setup(x => x.HttpContext).Returns(new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1")                                   
                }))
            });

            var createRoomOfferRequest = RoomFixture.CreateRoomRequestFixture();
            var createUser = UserFixture.CreateTestUser();

            using var dbContext = new AirbnbDatabaseFake().Context;
            var mockRepository = new RoomRepository(dbContext, mockHttpContext.Object);

            // Act
            var result = await mockRepository.CreateOffer(createRoomOfferRequest);

            // Assert
            result.Should().BeOfType<CreateRoomOfferResponse>();
            var savedOffer = await dbContext.Rooms.FindAsync(result.Id);
            savedOffer.Should().NotBeNull();
            savedOffer.Should().BeOfType<Room>();
        }

        [Fact]
        public async Task Remove_WhenRoomExists_ShouldReturnTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AirbnbDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AirbnbDbContext(options))
            {
                var room = new Room { Id = 1 };
                context.Rooms.Add(room);
                context.SaveChanges();
            }

            using (var context = new AirbnbDbContext(options))
            {
                var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                var roomRepository = new RoomRepository(context, httpContextAccessorMock.Object);

                // Act
                var result = await roomRepository.Remove(1);

                // Assert
                Assert.True(result);
            }
        }

        [Fact]
        public async Task Remove_WhenRoomDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AirbnbDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AirbnbDbContext(options))
            {
                context.Rooms.Add(new Room { Id = 2 });
                context.SaveChanges();
            }

            using (var context = new AirbnbDbContext(options))
            {
                var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                var roomRepository = new RoomRepository(context, httpContextAccessorMock.Object);

                // Act
                var result = await roomRepository.Remove(54444); 

                // Assert
                Assert.False(result);
            }
        }









    }
}
