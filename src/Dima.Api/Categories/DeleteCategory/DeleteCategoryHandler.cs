using Dima.Api.Common;
using Dima.Core.Categories;
using Dima.Core.Categories.DeleteCategory;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Categories.DeleteCategory;

/// <inheritdoc cref="IDeleteCategoryHandler"/>
internal sealed class DeleteCategoryHandler(
    DimaDbContext context,
    IValidator<DeleteCategoryRequest> validator)
    : IDeleteCategoryHandler
{
    /// <summary>
    /// Handles the delete category request asynchronously.
    /// </summary>
    /// <param name="request">The delete category request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating the outcome of the delete operation,
    /// or validation errors if the request is invalid.
    /// </returns>
    public async Task<Result> HandleAsync(
        DeleteCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validatorResult.IsValid)
        {
            return Result.Invalid(
                validatorResult
                    .AsErrors()
            );
        }

        Category? category = await context
            .Categories
            .FirstOrDefaultAsync(
                category => category.Id == request.Id,
                cancellationToken);

        if (category is null)
        {
            return Result.NotFound(CategoryErrors.NotFound(request.Id));
        }

        context.Categories.Remove(category);
        context.SaveChanges();

        return Result.NoContent();

    }
}
