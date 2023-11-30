using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Models;
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
        [Fact]
        public async Task WhenGetAllReservations_Returns200()
        {
            // Arrange 
            var roomServiceMock = new Mock<IRoomService>();
            var roomsController = new RoomsController(roomServiceMock.Object);


            // Act 
            var result = await roomsController.GetAllRooms();

            // Assert


            result.Should().BeOfType<ActionResult<List<ListOfRoomsResponse>>>();
            var objectResult = (ActionResult<List<ListOfRoomsResponse>>)result;
            objectResult.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)objectResult.Result;
            okObjectResult.StatusCode.Should().Be(200);

        }

        [Theory]
        [InlineData(1)]
        public async Task WhenRemoveSingleRoom_Returns200(int roomId)
        {
            // Arrange 
            var roomService = new Mock<IRoomService>(); 
            var roomController = new RoomsController(roomService.Object);

            // Act 
            var result = await roomController.RemoveRoom(roomId);

            // Assert 

            result.Should().BeOfType<ActionResult<bool>>();
            
            var objectResult = (ActionResult<bool>)result;
            objectResult.Result.Should().BeOfType<OkObjectResult>();

            var okObjectResult = (OkObjectResult)objectResult.Result;
            okObjectResult.StatusCode.Should().Be(200);


        }
    }
}
