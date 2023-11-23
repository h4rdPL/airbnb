using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
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

        [Fact]
        public async Task MakeReservation_WhenValidReservationRequest_ShouldAddToDatabaseAndReturnMakeReservationResponse()
        {
            // Arrange
            var mockHttpContext = new Mock<IHttpContextAccessor>();
            var userRepository = new Mock<IUserRepository>(); 

            var reservationRequest = new MakeReservationRequest
            {
                RoomId = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(3),
                ReservedGuests = 2
            };

            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, "123");
            mockHttpContext.Setup(x => x.HttpContext.User.Claims).Returns(new List<Claim> { userIdClaim });

            var dbContextOptions = new DbContextOptionsBuilder<AirbnbDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var dbContext = new AirbnbDbContext(dbContextOptions))
            {
                var roomRepository = new RoomRepository(dbContext, mockHttpContext.Object);

                // Act
                var result = await roomRepository.MakeReservation(reservationRequest);

                // Assert
                result.Should().NotBeNull();
                result.UserId.Should().Be(123); 
                result.StartDate.Should().Be(reservationRequest.StartDate);
                result.EndDate.Should().Be(reservationRequest.EndDate);
                result.FinalPrice.Should().Be(400);
                result.ReservedGuests.Should().Be(reservationRequest.ReservedGuests);

                var reservationInDatabase = await dbContext.Reservations.FirstOrDefaultAsync();
                reservationInDatabase.Should().NotBeNull();
                reservationInDatabase.UserId.Should().Be(123); 
                reservationInDatabase.RoomId.Should().Be(reservationRequest.RoomId);
                reservationInDatabase.StartDate.Should().Be(reservationRequest.StartDate);
                reservationInDatabase.EndDate.Should().Be(reservationRequest.EndDate);
                reservationInDatabase.FinalPrice.Should().Be(400); 
                reservationInDatabase.ReservedGuests.Should().Be(reservationRequest.ReservedGuests);
            }
        }
    }
}
