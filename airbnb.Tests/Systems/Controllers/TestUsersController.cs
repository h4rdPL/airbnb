using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Domain.Models;
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


    }
}
