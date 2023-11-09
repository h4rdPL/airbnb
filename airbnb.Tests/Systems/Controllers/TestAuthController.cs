using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Application.Common.Services;
using airbnb.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Moq;

namespace airbnb.Tests.Systems.Controller;

public class UnitTest1
{
    [Theory]
    [InlineData("test@gmail.com", "password")]
    public async Task Post_OnSuccess_Returns_StatusCode200(string email, string password)
    {
        //Arange
        var mockUsersService = new Mock<IUsersService>();
        var sut = new UsersController(mockUsersService.Object);

        //Act

        var result = (OkObjectResult)await sut.Register(email, password);

        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("test@gmail.com", "password")]
    public async Task Post_OnSuccess_InvokesUserServiceExactlyOnce(string email, string password)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
        .Setup(service => service.Register(email, password))
        .ReturnsAsync(new User());
        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Register(email, password);
        
        // Assert
        mockUsersService.Verify(
            service => service.Register(email, password),
            Times.Once());
    }

    [Theory]
    [InlineData("test@gmail.com", "password")]
    public async Task Post_OnSuccess_RegisterUser(string email, string password)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.Register(email, password))
            .ReturnsAsync(new User());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Register(email, password);

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        // cast to get access to the value of user object
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<Task<User>>();
    }

    [Theory]
    [InlineData("jan.kowalski@example.com")]
    [InlineData("jan_kowalski@example.com")]
    public async Task Get_OnInValidEmail_ReturnsFalse(string email)
    {
        // Arrange
        var mockEmailService = new Mock<IEmailService>();
        mockEmailService
            .Setup(service => service.IsValidEmail(email))
            .ReturnsAsync(true);

        var sut = new EmailService();

        // Act
        var result = await sut.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("jan@")]
    [InlineData("jan.kowalski@example")]
    [InlineData("jan@.com")]
    [InlineData("jan@example . com")]
    public async Task Get_OnValidEmail_ReturnsTrue(string email)
    {
        // Arrange
        var mockEmailService = new Mock<IEmailService>();
        mockEmailService
            .Setup(service => service.IsValidEmail(email))
            .ReturnsAsync(false);

        var sut = new EmailService();

        // Act
        var result = await sut.IsValidEmail(email);

        // Assert
        result.Should().BeFalse();
    }
}