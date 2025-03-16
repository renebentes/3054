using FluentValidation.Results;

namespace Dima.Core.Primitives;
public static class FluentValidationExtensions
{
    public static IEnumerable<Error> ToErrors(this List<ValidationFailure> failures)
    {
        List<Error> errors = [];

        failures.ForEach(failure =>
        {
            errors.Add(new Error(
                        $"Validation.{failure.PropertyName}.{failure.ErrorCode}",
                        failure.ErrorMessage));
        });

        return errors;
    }
}
