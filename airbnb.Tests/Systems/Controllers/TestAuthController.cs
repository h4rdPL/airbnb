using airbnb.API.Controllers;
using airbnb.Application.Common.Interfaces;
using airbnb.Application.Services;
using airbnb.Domain.Models;
using airbnb.Tests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace airbnb.Tests.Systems.Controller;

public class UnitTest1
{
    [Theory]
    [InlineData("test@gmail.com", "password", "John", "Doe")]
    public async Task Post_OnSuccess_Returns_StatusCode200(string email, string password, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.Register(email, password, FirstName, LastName))
            .ReturnsAsync(UserFixture.CreateTestUser());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Register(email, password, FirstName, LastName);

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("test@gmail.com", "password", "John", "Doe")]
    public async Task Post_OnSuccess_InvokesUserServiceExactlyOnce(string email, string password, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
        .Setup(service => service.Register(email, password, FirstName, LastName))
        .ReturnsAsync(new User());
        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = (OkObjectResult)await sut.Register(email, password, FirstName, LastName);
        
        // Assert
        mockUsersService.Verify(
            service => service.Register(email, password, FirstName, LastName),
            Times.Once());
    }

    [Theory]
    [InlineData("test@gmail.com", "password", "John", "Doe")]
    public async Task Post_OnSuccess_RegisterUser(string email, string password, string FirstName, string LastName)
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.Register(email, password, FirstName, LastName))
            .ReturnsAsync(new User());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Register(email, password, FirstName, LastName);

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