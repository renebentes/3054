using DerivedResult = Dima.Core.Primitives.Result.Result;

namespace Dima.UnitTests.Core.Primitives.Result;

public class ResultTests
{
    [Fact]
    public void Created_ShouldReturnResultWithCreatedStatus()
    {
        // Act
        var result = DerivedResult.Created();

        // Assert
        result
            .IsSuccess
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.Created);
        result
            .Errors
            .ShouldBeEmpty();
    }

    [Fact]
    public void Failure_WithMultipleErrors_ShouldReturnResultWithFailureStatus()
    {
        // Arrange
        var errors = new List<Error>{
            new("error1", "error message"),
            new("error2", "error message")
        };

        // Act
        var result = DerivedResult.Failure(errors);

        // Assert
        result
            .IsSuccess
            .ShouldBeFalse();
        result
            .Status
            .ShouldBe(ResultStatus.Failure);
        result
            .Errors
            .ShouldBeEquivalentTo(errors);
    }

    [Fact]
    public void Failure_WithSingleError_ShouldReturnResultWithFailureStatus()
    {
        // Arrange
        var error = new Error("error", "error message");

        // Act
        var result = DerivedResult.Failure(error);

        // Assert
        result
            .IsSuccess
            .ShouldBeFalse();
        result
            .Status
            .ShouldBe(ResultStatus.Failure);

        result
            .Errors
            .ShouldContain(error);
    }

    [Fact]
    public void Invalid_WithMultipleErrors_ShouldReturnResultWithInvalidStatus()
    {
        // Arrange
        var errors = new List<Error>{
            new("invalid1", "invalid message"),
            new("invalid2", "invalid message")
        };

        // Act
        var result = DerivedResult.Invalid(errors);

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
    public void Invalid_WithSingleError_ShouldReturnResultWithInvalidStatus()
    {
        // Arrange
        var error = new Error("invalid", "invalid message");

        // Act
        var result = DerivedResult.Invalid(error);

        // Assert
        result
            .IsSuccess
            .ShouldBeFalse();
        result
            .Status
            .ShouldBe(ResultStatus.Invalid);
        result
            .Errors
            .ShouldContain(error);
    }

    [Fact]
    public void NoContent_ShouldReturnResultWithNoContentStatus()
    {
        // Act
        var result = DerivedResult.NoContent();

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
    public void Success_ShouldReturnResultWithOkStatus()
    {
        // Act
        var result = DerivedResult.Success();

        // Assert
        result
            .IsSuccess
            .ShouldBeTrue();
        result
            .Status
            .ShouldBe(ResultStatus.Ok);
        result
            .Errors
            .ShouldBeEmpty();
    }
}
