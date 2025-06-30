using Dima.Api.Common;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Categories.UpdateCategory;

/// <summary>
/// Handles the update of an existing category.
/// </summary>
/// <remarks>
/// This handler validates the incoming <see cref="UpdateCategoryRequest"/>, updates the corresponding <see cref="Category"/> entity,
/// persists the changes to the database, and returns the result of the operation.
/// </remarks>
/// <param name="context">The application database context.</param>
/// <param name="validator">The validator for the update category request.</param>
public class UpdateCategoryHandler(
    IApplicationDbContext context,
    IValidator<UpdateCategoryRequest> validator)
    : IUpdateCategoryHandler
{
    /// <summary>
    /// Handles the update category request asynchronously.
    /// </summary>
    /// <param name="request">The update category request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A <see cref="Result"/> indicating the outcome of the update operation,
    /// or validation errors if the request is invalid.
    /// </returns>
    public async Task<Result> HandleAsync(
        UpdateCategoryRequest request,
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

        var category = await context.Categories.SingleOrDefaultAsync(
            c => c.Id == request.Id,
            cancellationToken);

        if (category is null)
        {
            return Result.NotFound(DomainErrors.Category.NotFound);
        }

        category.ChangeTitle(request.Title);
        category.ChangeDescription(request.Description);

        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
