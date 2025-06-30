using DerivedResult = Dima.Core.Primitives.Result.Result;

namespace Dima.UnitTests.Core.Primitives.Result;

public class ResultTTests
{
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
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.Created);
        result
            .Value
            .ShouldBe(value);
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

        var result = DerivedResult.Failure(errors);

        // Act
        Result<string> resultWithErrors = result;

        // Assert
        resultWithErrors
            .IsSuccess
            .ShouldBeFalse();
        resultWithErrors
            .Status
            .ShouldBe(result.Status);
        resultWithErrors
            .Errors
            .ShouldBeEquivalentTo(errors);
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
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.Ok);
        result
            .Value
            .ShouldBe(value);
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
            .ShouldBeFalse();
        result
            .Status
            .ShouldBe(ResultStatus.Invalid);
        result
            .Errors
            .ShouldContain(e => e == error);
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
            .ShouldBeFalse();
        result
            .Status
            .ShouldBe(ResultStatus.Invalid);
        result
            .Errors
            .ShouldBeEquivalentTo(errors);
    }

    [Fact]
    public void NoContent_ShouldReturnResultWithNoContentStatus()
    {
        // Act
        var result = Result<int>.NoContent();

        // Assert
        result
            .IsSuccess
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.NoContent);
        result
            .Errors
            .ShouldBeEmpty();
    }
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
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.Ok);
        result
            .Value
            .ShouldBe(value);
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
        action.ShouldThrow<InvalidOperationException>();
    }
}
