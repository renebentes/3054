using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

internal static class CreateCategoryEndpoint
{
    internal static RouteHandlerBuilder MapCreateCategoryEndpoint(this IEndpointRouteBuilder endpoint)
        => endpoint.MapPost("/", HandleAsync)
                   .WithName("Categories: Create")
                   .WithSummary("Create a new category.")
                   .WithDescription("Create a new category.")
                   .Produces<Result<long>>(StatusCodes.Status201Created)
                   .Produces<Error>(StatusCodes.Status400BadRequest)
                   .Produces<Error>(StatusCodes.Status500InternalServerError);

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
