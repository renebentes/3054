using Dima.Api.Common;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;
using FluentValidation;

namespace Dima.Api.Categories.UpdateCategory;

/// <summary>
/// Handles the update category operations.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="UpdateCategoryHandler"/> class.
/// </remarks>
/// <param name="context">The database context.</param>
/// <param name="validator">The validator for the update category request.</param>
public class UpdateCategoryHandler(
    DimaDbContext context,
    IValidator<UpdateCategoryRequest> validator)
    : IUpdateCategoryHandler
{
    /// <summary>
    /// Handles the update category request asynchronously.
    /// </summary>
    /// <param name="request">The update category request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
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

        var category = await context.Categories
            .FindAsync(
            [request.Id],
            cancellationToken: cancellationToken);

        if (category is null)
        {
            return Result.NotFound(DomainErrors.Category.NotFound);
        }

        category.ChangeTitle(request.Title);
        category.ChangeDescription(request.Description);

        context.Update(category);
        await context.SaveChangesAsync(cancellationToken);

        return Result.NoContent();
    }
}
