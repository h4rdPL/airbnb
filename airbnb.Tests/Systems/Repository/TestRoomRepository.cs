using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;
using airbnb.Infrastructure.Rooms;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
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
            var createRoomOfferRequest = RoomFixture.CreateRoomRequestFixture();
            
            mockHttpContext.Setup(x => x.HttpContext.User.Claims)
                .Returns(new List<Claim> { new Claim(ClaimTypes.NameIdentifier, "123") });

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

    }
}
