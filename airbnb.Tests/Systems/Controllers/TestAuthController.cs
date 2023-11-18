using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Controller;

public class TestAuthController
{

    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_WhenRegisterUser_Returns_StatusCode200(string email, string password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.Register(email, password, RepeatedPassword, FirstName, LastName))
            .ReturnsAsync(new User()); 

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Register(email, password, RepeatedPassword, FirstName, LastName);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.StatusCode.Should().Be(200);
    }


    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_InvokesUserService_ExactlyOnce(string email, string password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
        .Setup(service => service.Register(email, password, RepeatedPassword, FirstName, LastName))
        .ReturnsAsync(UserFixture.CreateTestUser());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Register(email, password, RepeatedPassword, FirstName, LastName);
        
        // Assert
        mockUsersService.Verify(
            service => service.Register(email, password, RepeatedPassword, FirstName, LastName),
            Times.Once());
    }

    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_WhenUserIsRegister_IsTypeOfUser(string email, string password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.Register(email, password, RepeatedPassword, FirstName, LastName))
            .ReturnsAsync(new User());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Register(email, password, RepeatedPassword, FirstName, LastName);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var objectResult = (OkObjectResult)result;
        var user = objectResult.Value.Should().BeAssignableTo<User>().Subject;
        // Additional assertions on the 'user' object if needed
    }

}