using Dima.Api.Common;
using Dima.Core.Categories.UpdateCategory;

namespace Dima.Api.Categories.UpdateCategory;

/// <summary>
/// Maps the endpoint for updating an existing category to the specified <see cref="IEndpointRouteBuilder"/>.
/// </summary>
/// <remarks>This method maps a PUT endpoint at the route "/{id:long}" for updating an existing category. The
/// endpoint expects a category ID as a route parameter and an <see cref="UpdateCategoryRequest"/> object in the request
/// body.</remarks>
internal static class UpdateCategoryEndpoint
{
    /// <summary>
    /// Maps the endpoint for updating an existing category.
    /// </summary>
    /// <remarks>This endpoint handles HTTP PUT requests to update an existing category by its ID.  It
    /// supports the following response types:
    /// <list type="bullet">
    /// <item><description><see cref="Result{T}"/> with a status code of 204 (No Content) if the update is successful.</description></item>
    /// <item><description><see cref="Error"/> with a status code of 400 (Bad Request) if the request is invalid.</description></item>
    /// <item><description><see cref="Error"/> with a status code of 404 (Not Found) if the category does not exist.</description></item>
    /// <item><description><see cref="Error"/> with a status code of 500 (Internal Server Error) if an unexpected error occurs.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="endpoint">The <see cref="IEndpointRouteBuilder"/> to which the endpoint is added.</param>
    /// <returns>The <see cref="IEndpointRouteBuilder"/> with the update category endpoint mapped.</returns>
    internal static IEndpointRouteBuilder MapUpdateCategoryEndpoint(this IEndpointRouteBuilder endpoint)
    {
        endpoint
            .MapPut("/{id:long}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Update an existing category.")
            .WithDescription("Update an existing category.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<Result>(StatusCodes.Status400BadRequest)
            .Produces<Error>(StatusCodes.Status404NotFound)
            .Produces<Error>(StatusCodes.Status500InternalServerError);

        return endpoint;
    }

    /// <summary>
    /// Processes an update request for a category and returns the result.
    /// </summary>
    /// <param name="handler">The handler responsible for processing the update request.</param>
    /// <param name="id">The unique identifier of the category to be updated.</param>
    /// <param name="request">The update request containing the new category data. The <see cref="Id"/> property will be set to the provided
    /// <paramref name="id"/>.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IResult"/>
    /// representing the outcome of the update operation.</returns>
    private static async Task<IResult> HandleAsync(
        IUpdateCategoryHandler handler,
        long id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        request = request with { Id = id };

        var result = await handler.HandleAsync(request, cancellationToken);
        return result.ToResult();
    }
}
