using Dima.Core.Primitives;

namespace Dima.UnitTests.Core.Primitives;

public class ErrorTests
{
    [Fact]
    public void ImplicitOperatorShouldReturnCodeFromError()
    {
        // Arrange
        var error = new Error("Error.Test", "Error description");

        // Act
        string code = error;

        // Assert
        code.Should().Be(error.Code);
    }

    [Fact]
    public void ToStringShouldReturnMessageFromError()
    {
        // Arrange
        var error = new Error("Error.Test", "Error description");

        // Act
        string result = error.ToString();

        // Assert
        result.Should().Be(error.Message);
    }
}
