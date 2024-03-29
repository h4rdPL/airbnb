using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.Authentication;
using airbnb.Contracts.Authentication.LoginResponse;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Controller;

public class TestAuthController
{

    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_WhenRegisterUser_Returns_StatusCode200(string Email, string Password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        var mockEmailService = new Mock<IEmailService>();
        var registerRequest = new AuthenticationRequest(FirstName, LastName, Email, Password, RepeatedPassword);
        var authResponse = new AuthResponse(FirstName, LastName, Email);
        mockUsersService
            .Setup(service => service.Register(registerRequest))
            .ReturnsAsync(authResponse);

        var sut = new UsersController(mockUsersService.Object, mockEmailService.Object);

        // Act
        var result = await sut.Register(registerRequest);

        // Assert
        var objectResult = (ActionResult<AuthResponse>)result;
        objectResult.Result.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult)objectResult.Result;
        okObjectResult.StatusCode.Should().Be(200);
        okObjectResult.Value.Should().BeOfType<AuthResponse>();
    }


    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_InvokesUserService_ExactlyOnce(string email, string password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        var mockEmailService = new Mock<IEmailService>();

        var registerRequest = new AuthenticationRequest(FirstName, LastName, email, password, RepeatedPassword);
        var authResponse = new AuthResponse(FirstName, LastName, email);

        mockUsersService
            .Setup(service => service.Register(registerRequest))
            .ReturnsAsync(authResponse);

        var sut = new UsersController(mockUsersService.Object, mockEmailService.Object);

        // Act
        var result = await sut.Register(registerRequest);

        // Assert
        mockUsersService.Verify(
            service => service.Register(registerRequest),
            Times.Once());
    }

    [Theory]
    [InlineData("test@gmail.com", "password", "password", "John", "Doe")]
    public async Task Post_OnSuccess_WhenUserIsRegister_IsTypeOfUser(string email, string password, string RepeatedPassword, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        var mockEmailService = new Mock<IEmailService>();

        var registerRequest = new AuthenticationRequest(FirstName, LastName, email, password, RepeatedPassword);
        var authResponse = new AuthResponse(FirstName, LastName, email);

        mockUsersService
            .Setup(service => service.Register(registerRequest))
            .ReturnsAsync(authResponse);

        var sut = new UsersController(mockUsersService.Object, mockEmailService.Object);

        // Act
        var result = await sut.Register(registerRequest);

        // Assert
        result.Should().BeOfType<ActionResult<AuthResponse>>();
        var objectResult =  (ActionResult<AuthResponse>)result; 
        objectResult.Result.Should().BeOfType<OkObjectResult>();
        var okObjectResult = (OkObjectResult)objectResult.Result;
        okObjectResult.StatusCode.Should().Be(200);
        okObjectResult.Value.Should().BeOfType<AuthResponse>();
    }


}