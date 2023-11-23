using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Controllers
{
    public class RoomsControllerTests
    {
        [Fact]
        public async Task MakeReservation_ReturnsStatusCode200()
        {
            // Arrange 
            var roomServiceMock = new Mock<IRoomService>();
            var roomsController = new RoomsController(roomServiceMock.Object);

            var makeReservationRequest = new MakeReservationRequest
            {
                RoomId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(6),
                ReservedGuests = 5,
            };


            // Act 
            var result = await roomsController.MakeReservation(makeReservationRequest);

            // Assert
            result.Should().BeOfType<ActionResult<MakeReservationResponse>>();
            var objectResult = (ActionResult<MakeReservationResponse>)result;
            objectResult.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)objectResult.Result;
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().BeOfType<MakeReservationResponse>();

        }
    }
}
