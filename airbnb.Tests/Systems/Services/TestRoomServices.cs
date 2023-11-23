using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Enum;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
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




    }
}
