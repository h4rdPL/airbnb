using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using airbnb.Infrastructure.Rooms;
using airbnb.Tests.Fixtures;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

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

            var createRoomOfferRequest = RoomFixture.CreateRoomRequestFixture();
            var createRoomOfferResponse = RoomFixture.CreateRoomResponseFixture();

            mockMapper.Setup(mapper => mapper.Map<CreateRoomOfferResponse>(It.IsAny<Room>()))
                      .Returns(createRoomOfferResponse);

            var sut = new RoomsController(mockService.Object);

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

        [Fact]
        public async Task MakeReservation_WhenValidReservationRequest_ShouldReturnMakeReservationResponse()
        {
            // Arrange
            var mockRoomRepository = new Mock<IRoomRepository>();
            var mockMapper = new Mock<IMapper>();

            var roomService = new RoomService(mockRoomRepository.Object, mockMapper.Object);

            var reservationRequest = new MakeReservationRequest
            {
                RoomId = 1,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(3),
                ReservedGuests = 2
            };

            var reservationResponse = new MakeReservationResponse
            {
                UserId = 123,
                RoomId = reservationRequest.RoomId,
                StartDate = reservationRequest.StartDate,
                EndDate = reservationRequest.EndDate,
                FinalPrice = 400,
                ReservedGuests = reservationRequest.ReservedGuests
            };

            mockRoomRepository.Setup(x => x.MakeReservation(reservationRequest)).ReturnsAsync(reservationResponse);

            mockMapper.Setup(x => x.Map<MakeReservationResponse>(It.IsAny<MakeReservationResponse>()))
                .Returns((MakeReservationResponse response) => response); 

            // Act
            var result = await roomService.MakeReservation(reservationRequest);

            // Assert
            result.Should().BeEquivalentTo(reservationResponse);

            mockRoomRepository.Verify(x => x.MakeReservation(It.Is<MakeReservationRequest>(r => r.RoomId == reservationRequest.RoomId)), Times.Once);
        }

        [Fact]
        public async Task MakeReservation_WhenValidReservationRequest_ShouldAddToDatabaseAndReturnMakeReservationResponse()
        {
            // Arrange
            var mockHttpContext = new Mock<IHttpContextAccessor>();
            var mockRepository = new Mock<IRoomRepository>();
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
                mockRepository.Setup(repo => repo.GetRoomById(It.IsAny<int>()))
                    .ReturnsAsync(new Room());

                // Act
                var result = await mockRepository.Object.MakeReservation(reservationRequest);

                // Assert
                result.Should().NotBeNull();
                result.UserId.Should().Be(0);
                result.ReservedGuests.Should().Be(0);

                var reservationInDatabase = await dbContext.Reservations.FirstOrDefaultAsync();
            }
        }









    }
}
