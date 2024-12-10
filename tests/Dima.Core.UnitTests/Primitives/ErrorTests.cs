using Dima.Core.Primitives;
using FluentAssertions;

namespace Dima.Core.UnitTests.Primitives;

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
}
