using FluentValidation.Results;

namespace Dima.Api.Common;

/// <summary>
/// Extension methods for FluentValidation.
/// </summary>
public static class FluentValidationExtensions
{
    /// <summary>
    /// Converts a <see cref="ValidationResult"/> to a collection of <see cref="Error"/> objects.
    /// </summary>
    /// <param name="validationResult">The validation result to convert.</param>
    /// <returns>An enumerable collection of <see cref="Error"/> objects representing the validation errors.</returns>
    public static IEnumerable<Error> AsErrors(this ValidationResult validationResult)
    {
        List<Error> errors = [];

        validationResult.Errors.ForEach(failure =>
            errors.Add(
                new Error(
                    $"Validation.{failure.PropertyName}.{failure.ErrorCode}",
                    failure.ErrorMessage
                )
            )
        );

        return errors;
    }
}
