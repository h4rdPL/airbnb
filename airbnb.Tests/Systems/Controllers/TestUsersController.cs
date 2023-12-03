using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Controllers
{
    public class TestUsersController
    {
        [Fact]
        public async Task WhenUserPostCommentCorrectly_Returns200()
        {
            // Arrange
            var authService = new Mock<IUsersService>();
            var usersController = new UsersController(authService.Object);

            var postNewComment = new CreateCommentsRequest
            {
                UserFirstName = "John",
                Comment = "Lorem ipsum dolor sit amet",
                NumberOfStars = 5,
                CreatedAt = new DateTime()
            };

            // Act 
            var result = await usersController.CreateComment(postNewComment);

            // Assert
            result.Should().NotBeNull();

            result.Should().BeOfType<ActionResult<CreateCommentResponse>>().Which.Value.Should().BeOfType<CreateCommentResponse>();
        }
        [Fact]
        public async Task WhenDeleteUserCorrectly_ShouldReturn200()
        {
            // Arrange
            var authService = new Mock<IUsersService>();
            var usersController = new UsersController(authService.Object);
            var user = UserFixture.CreateTestUser();
            var userEmail = user.Email;

            // Act 
            var result = await usersController.DeleteUser(userEmail);

            // Assert
            var objectResult = (ActionResult<bool>)result;
            objectResult.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)objectResult.Result;
            okObjectResult.StatusCode.Should().Be(200);
            okObjectResult.Value.Should().BeOfType<bool>();
        }
    }
}
