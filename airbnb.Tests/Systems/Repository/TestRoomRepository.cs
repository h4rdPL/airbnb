using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Enum;
using airbnb.Infrastructure.Rooms;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using airbnb.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Castle.Core.Logging;

namespace airbnb.Tests.Systems.Repository
{
    public class TestRoomRepository
    {
        [Fact]
        public async Task CreateOffer_WhenValidOfferProvided_ShouldAddToDatabase()
        {
            // Arrange
            var testRoom = OfferFixture.CreateTestRoom();
            var createRoomOfferRequest = new CreateRoomOfferRequest
            {
                HomeType = HomeType.House,
                TotalBathrooms = 1,
                TotalOcupancy = 1,
                TotalBedrooms = 1,
                Summary = 3,
                Address = new Address
                {
                    ZIPCode = "35-123",
                    City = "Example city",
                    Street = "Example street",
                    ApartmentNumber = 54
                },
                Amenities = new Amenities
                {
                    HasAirCon = true,
                    HasHeating = true,
                    HasInternet = true,
                    HasKitchen = true,
                    HasTV = true,
                },
                Price = 300,
                PublishedAt = new DateTime()
            };

            using var dbContext = new AirbnbDatabaseFake().Context;
            var mockRepository = new RoomRepository(dbContext); 

            // Act
            var result = await mockRepository.CreateOffer(createRoomOfferRequest);

            // Assert
            result.Should().BeOfType<Room>();
            var savedOffer = await dbContext.Rooms.FindAsync(testRoom.TotalOccupancy);
            savedOffer.Should().NotBeNull();
            savedOffer.Should().BeOfType<Room>();
        }
    }
}
