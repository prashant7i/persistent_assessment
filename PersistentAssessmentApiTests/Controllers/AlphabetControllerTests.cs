using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PersistentAssessmentApi.Controllers;
using Xunit;

/// <summary>
/// A unit test class for AlphabetController
/// </summary>

public class AlphabetControllerTests
{
    private readonly Mock<ILogger<AlphabetController>> _mockLogger;

    private readonly AlphabetController _alphabetController;

    public AlphabetControllerTests()
    {
        _mockLogger = new Mock<ILogger<AlphabetController>>();

        _alphabetController = new AlphabetController(_mockLogger.Object);
    }

    /// <summary>
    /// Unit test to check if method returns true when alphabets are present
    /// </summary>
    [Fact]
    public void Should_ReturnTrue_WhenAllAreAlphabet()
    {
        //Act
        var result = _alphabetController.CheckAlpabet("abcdefghijklmnopqrstuvwxyz");

        //Assert
        result.Value.Should().BeTrue();
    }

    /// <summary>
    /// Unit test to check if method returns bad request for invalid input
    /// </summary>
    /// <param name="input"></param>
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Should_ReturnBadRequest_WhenInputIsNullOrEmpty(string input)
    {
        //Arrange
        var expectedMessage = "Input string cannot be null or empty";

        //Act
        var result = _alphabetController.CheckAlpabet(input);

        //Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be(expectedMessage);
    }

    /// <summary>
    /// Unit test to check if method returns false for all other use cases where input
    /// doesn't contain all alphabets
    /// </summary>
    /// <param name="input"></param>
    [Theory]
    [InlineData("Test123")]
    [InlineData("Test@123")]
    [InlineData("###")]
    [InlineData("12345")]
    public void Should_ReturnFalse_WhenAllAreNotAlphabet(string input)
    {
        //Act
        var result = _alphabetController.CheckAlpabet(input);

        //Assert
        result.Value.Should().BeFalse();
    }
}