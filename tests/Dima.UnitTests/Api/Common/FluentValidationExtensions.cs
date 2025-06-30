using Dima.Api.Common;
using FluentValidation.Results;

namespace Dima.UnitTests.Api.Common;

public class FluentValidationExtensions
{
    [Fact]
    public void AsErrors_ShouldReturnErrors()
    {
        // Arrange
        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("Property", "Error message"));

        // Act
        var errors = validationResult.AsErrors().ToArray();

        // Assert
        errors
            .ShouldNotBeNull();
        errors
            .ShouldContain(new Error("Validation.Property.", "Error message"));
    }
}
