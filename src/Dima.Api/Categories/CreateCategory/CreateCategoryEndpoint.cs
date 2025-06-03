using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

/// <summary>
/// Provides an extension method to map the "Create Category" endpoint to an <see cref="IEndpointRouteBuilder"/>.
/// </summary>
internal static class CreateCategoryEndpoint
{
    /// <summary>
    /// Maps the endpoint for creating a new category to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    /// <remarks>This endpoint handles HTTP POST requests to create a new category. It returns a result
    /// containing the ID of the newly created category upon success, or an error response if the request is invalid or
    /// an internal server error occurs.</remarks>
    /// <param name="endpoint">The <see cref="IEndpointRouteBuilder"/> to which the endpoint is added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the create category endpoint mapped.</returns>
    internal static IEndpointRouteBuilder MapCreateCategoryEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Create a new category.")
            .WithDescription("Create a new category.")
            .Produces<Result<long>>(StatusCodes.Status201Created)
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status500InternalServerError);

        return endpoint;
    }

    /// <summary>
    /// Processes a request to create a new category and returns an appropriate HTTP result.
    /// </summary>
    /// <param name="handler">The handler responsible for processing the category creation request.</param>
    /// <param name="request">The request containing the details of the category to be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>An <see cref="IResult"/> representing the HTTP response. Possible responses include:
    /// <list type="bullet">
    /// <item><description><see cref="TypedResults.Created"/> if the category is successfully
    /// created.</description></item>
    /// <item><description><see cref="TypedResults.BadRequest"/> if the request is
    /// invalid.</description></item>
    /// <item><description><see cref="TypedResults.StatusCode"/> with a 500 status code
    /// for unexpected errors.</description></item> </list></returns>
    /// <exception cref="NotImplementedException">Thrown if the result status is <see cref="ResultStatus.Ok"/> or <see cref="ResultStatus.Failure"/>, as these
    /// cases are not yet implemented.</exception>
    private static async Task<IResult> HandleAsync(
        ICreateCategoryHandler handler,
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(request, cancellationToken);
        return result.Status switch
        {
            ResultStatus.Created => TypedResults.Created($"/categories/{result.Value}"),
            ResultStatus.Invalid => TypedResults.BadRequest(result.Errors),
            ResultStatus.Ok => throw new NotImplementedException(),
            ResultStatus.Failure => throw new NotImplementedException(),
            _ => TypedResults.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}
