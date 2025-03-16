namespace Dima.UnitTests.Core.Primitives.Result;

public class ResultTTests
{
    [Fact]
    public void Success_ShouldHaveStatusOk()
    {
        // Arrange
        const string value = "Test Value";

        // Act
        var result = Result<string>.Success(value);

        // Assert
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Status
            .Should()
            .Be(ResultStatus.Ok);
        result
            .Value
            .Should()
            .Be(value);
    }

    [Fact]
    public void Created_ShouldHaveStatusCreated()
    {
        // Arrange
        const string value = "Test Value";

        // Act
        var result = Result<string>.Created(value);

        // Assert
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Status
            .Should()
            .Be(ResultStatus.Created);
        result
            .Value
            .Should()
            .Be(value);
    }

    [Fact]
    public void Invalid_ShouldHaveStatusInvalid()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");

        // Act
        var result = Result<string>.Invalid(error);

        // Assert
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Status
            .Should()
            .Be(ResultStatus.Invalid);
        result
            .Errors
            .Should()
            .ContainSingle(e => e == error);
    }

    [Fact]
    public void Invalid_WithMultipleErrors_ShouldHaveStatusInvalid()
    {
        // Arrange
        var errors = new List<Error>
        {
            new("Error Code 1", "Error Message 1"),
            new("Error Code 2", "Error Message 2")
        };

        // Act
        var result = Result<string>.Invalid(errors);

        // Assert
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Status
            .Should()
            .Be(ResultStatus.Invalid);
        result
            .Errors
            .Should()
            .BeEquivalentTo(errors);
    }

    [Fact]
    public void Value_ShouldThrowException_WhenResultIsFailure()
    {
        // Arrange
        var error = new Error("Error Code", "Error Message");
        var result = Result<string>.Invalid(error);

        // Act
        Action action = () => _ = result.Value;

        // Assert
        action
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Cannot access value for a failure result.");
    }

    [Fact]
    public void ImplicitConversion_ShouldReturnSuccessResult()
    {
        // Arrange
        const string value = "Test Value";

        // Act
        Result<string> result = value;

        // Assert
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Status
            .Should()
            .Be(ResultStatus.Ok);
        result
            .Value
            .Should()
            .Be(value);
    }

    [Fact]
    public void ImplicitConversionFromResult_ShouldReturnResultWithSameStatusAndErrors()
    {
        // Arrange
        var errors = new List<Error>
        {
            new("Error Code 1", "Error Message 1"),
            new("Error Code 2", "Error Message 2")
        };

        var result = Dima.Core.Primitives.Result.Result.Failure(errors);

        // Act
        Result<string> resultWithErrors = result;

        // Assert
        resultWithErrors
            .IsSuccess
            .Should()
            .BeFalse();
        resultWithErrors
            .Status
            .Should()
            .Be(result.Status);
        resultWithErrors
            .Errors
            .Should()
            .BeEquivalentTo(errors);
    }
}
