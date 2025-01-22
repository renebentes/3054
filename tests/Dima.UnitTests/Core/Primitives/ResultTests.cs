using Dima.Core.Primitives;
using Dima.Core.Primitives.Result;

namespace Dima.UnitTests.Core.Primitives;

public class ResultTests
{
    [Fact]
    public void Failure_WithMultipleErrors_ShouldReturnResultWithFailureStatus()
    {
        // Arrange
        var errors = new List<Error>{
            new("error1", "error message"),
            new("error2", "error message")
        };

        // Act
        var result = Result.Failure([.. errors]);

        // Assert
        result.IsSuccess.Should()
            .BeFalse();
        result.Status.Should()
            .Be(ResultStatus.Failure);
        result.Errors.Should()
            .BeEquivalentTo(errors);
    }

    [Fact]
    public void Failure_WithSingleError_ShouldReturnResultWithFailureStatus()
    {
        // Arrange
        var error = new Error("error", "error message");

        // Act
        var result = Result.Failure(error);

        // Assert
        result.IsSuccess.Should()
            .BeFalse();
        result.Status.Should()
            .Be(ResultStatus.Failure);
        result.Errors.Should()
            .ContainSingle().Which
            .Should()
            .BeEquivalentTo(error);
    }

    [Fact]
    public void Success_ShouldReturnResultWithOkStatus()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Status.Should().Be(ResultStatus.Ok);
        result.Errors.Should().BeEmpty();
    }
}
