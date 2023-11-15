using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Enum;
using airbnb.Domain.Models;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Services
{
    public class TestRoomServices
    {
        [Fact]
        public async Task Post_OnCreateOffer_Returns200()
        {
            // Arrange 
            var mockService = new Mock<IRoomService>();
            var mockMapper = new Mock<IMapper>();

            // Przygotuj dane testowe
            var createRoomOfferRequest = new CreateRoomOfferRequest
            {
                HomeType = HomeType.House,
                TotalOcupancy = 2,
                TotalBedrooms = 1,
                TotalBathrooms = 1,
                Summary = 300,
                Address = new Address
                {
                    ZIPCode = "12345",
                    City = "Example City",
                    Street = "Main Street",
                    ApartmentNumber = 101
                },
                Amenities = new Amenities
                {
                    HasTV = true,
                    HasKitchen = true,
                    HasHeating = true,
                    HasInternet = true,
                    HasAirCon = true
                },
                Price = 100,
                PublishedAt = DateTime.UtcNow
            };

            var createRoomOfferResponse = new CreateRoomOfferResponse
            {
                Id = 1,
                HomeType = HomeType.Apartment,
                TotalOcupancy = 2,
                TotalBedroom = 1,
                TotalBathrooms = 1,
                Summary = 0,
                Address = new Address
                {
                    ZIPCode = "12345",
                    City = "Example City",
                    Street = "Main Street",
                    ApartmentNumber = 101
                },
                Amenities = new Amenities
                {
                    HasTV = true,
                    HasKitchen = true,
                    HasHeating = true,
                    HasInternet = true,
                    HasAirCon = true
                },
                Price = 100,
                PublishedAt = DateTime.UtcNow,
            };

            mockMapper.Setup(mapper => mapper.Map<CreateRoomOfferResponse>(It.IsAny<Room>()))
                      .Returns(createRoomOfferResponse);

            var sut = new RoomsController(mockService.Object, mockMapper.Object);

            // Act
            var result = await sut.CreateRoomOffer(createRoomOfferRequest);

            // Assert
            result.Should().BeOfType<ActionResult<CreateRoomOfferResponse>>();
            var objectResult = (ActionResult<CreateRoomOfferResponse>)result;
            objectResult.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)objectResult.Result;
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().BeOfType<CreateRoomOfferResponse>();

        }




    }
}
