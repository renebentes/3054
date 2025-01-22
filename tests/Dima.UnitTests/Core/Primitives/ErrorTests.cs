using Dima.Core.Primitives;

namespace Dima.UnitTests.Core.Primitives;

public class ErrorTests
{
    [Fact]
    public void ImplicitConversion_ReturnsErrorCode()
    {
        // Arrange
        var error = new Error("Error.Test", "Error description");

        // Act
        string code = error;

        // Assert
        code.Should().Be(error.Code);
    }

    [Fact]
    public void ToString_ReturnsErrorMessage()
    {
        // Arrange
        var error = new Error("Error.Test", "Error description");

        // Act
        string result = error.ToString();

        // Assert
        result.Should().Be(error.Message);
    }

    [Fact]
    public void TwoDifferentErrors_AreNotEqual()
    {
        // Arrange
        var error1 = new Error("Error.404", "Not Found");
        var error2 = new Error("Error.500", "Internal Server Error");

        // Act & Assert
        error1.Should().NotBe(error2);
    }

    [Fact]
    public void TwoIdenticalErrors_AreEqual()
    {
        // Arrange
        var error1 = new Error("error", "Error description");
        var error2 = new Error("error", "Error description");

        // Act & Assert
        error1.Should().Be(error2);
    }
}
