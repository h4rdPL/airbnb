using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using FluentAssertions;
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
        }
    }
}
