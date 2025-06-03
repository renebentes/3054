using System.Text;

namespace Dima.Api.Common;

/// <summary>
/// Provides extension methods for <see cref="Result"/> objects/>.
/// </summary>
internal static class ResultExtensions
{
    /// <summary>s
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/> based on its status.
    /// </summary>
    /// <param name="result">The <see cref="Result"/> to convert.</param>
    /// <returns>An <see cref="IResult"/> representing the HTTP response.</returns>
    internal static IResult ToResult(this Result result, string route = "")
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            ResultStatus.Created => Results.Created($"{route}", result.Value),
            ResultStatus.NoContent => Results.NoContent(),
            ResultStatus.Invalid => Results.BadRequest(result.GenerateProblem),
            ResultStatus.NotFound => Results.NotFound(result.GenerateProblem),
            ResultStatus.Failure => Results.UnprocessableEntity(result.GenerateProblem),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    /// <summary>
    /// Converts a failed <see cref="Result"/> instance into an HTTP problem response.
    /// </summary>
    /// <remarks>This method generates an HTTP problem response based on the failure details of the provided
    /// <see cref="Result"/>. The response includes a status code, a title, a type URI, and detailed error information
    /// derived from the  <paramref name="result"/>. The status code and other metadata are determined by the <see
    /// cref="ResultStatus"/>  of the <paramref name="result"/>.</remarks>
    /// <param name="result">The <see cref="Result"/> instance to convert. Must represent a failure.</param>
    /// <returns>An <see cref="IResult"/> representing an HTTP problem response with details about the failure.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="result"/> represents a successful operation.</exception>
    private static IResult GenerateProblem(this Result result)
    {
        return result.IsSuccess
            ? throw new InvalidOperationException("Result is successful, cannot convert to problem.")
            : Results.Problem(
            detail: GetDetails(result.Errors),
            statusCode: GetStatusCode(result.Status),
            title: GetTitle(result.Status),
            type: GetType(result.Status));


        static string GetTitle(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid => "Invalid Request",
                ResultStatus.NotFound => "Resource Not Found",
                ResultStatus.Failure => "Operation Failed",
                _ => "Unknown Error"
            };

        static int GetStatusCode(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid => StatusCodes.Status400BadRequest,
                ResultStatus.NotFound => StatusCodes.Status404NotFound,
                ResultStatus.Failure => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };

        static string GetType(ResultStatus status)
            => status switch
            {
                ResultStatus.Invalid => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ResultStatus.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ResultStatus.Failure => "https://tools.ietf.org/html/rfc4918#section-11.2",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static string GetDetails(IEnumerable<Error> errors)
        {
            var details = new StringBuilder("Next error(s) occurred:");
            foreach (var error in errors)
            {
                details
                    .Append("- ")
                    .AppendLine(error.ToString());
            }

            return details.ToString();
        }
    }
}
