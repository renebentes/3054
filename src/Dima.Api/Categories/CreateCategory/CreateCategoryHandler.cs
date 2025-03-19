using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

/// <summary>
/// Handles the creation of a new category.
/// </summary>
/// <param name="context">The database context.</param>
/// <param name="validator">The validator for the create category request.</param>
internal sealed class CreateCategoryHandler(
    DimaDbContext context,
    IValidator<CreateCategoryRequest> validator)
    : ICreateCategoryHandler
{
    /// <summary>
    /// Handles the create category request asynchronously.
    /// </summary>
    /// <param name="request">The create category request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result containing the ID of the created category.</returns>
    public async Task<Result<long>> HandleAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validatorResult.IsValid)
        {
            return Result.Invalid(
                validatorResult
                    .Errors
                    .ToErrors()
            );
        }

        var title = new Title(request.Title);
        var category = new Category(title);
        category.ChangeDescription(request.Description);

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<long>.Created(category.Id);
    }
}
