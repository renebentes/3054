using Dima.Api.Common;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

/// <summary>
/// Handles the creation of a new <see cref="Category"/>.
/// </summary>
/// <remarks>
/// This handler validates the incoming <see cref="CreateCategoryRequest"/>, creates a new <see cref="Category"/> entity,
/// persists it to the database using the provided <see cref="ICategoryRepository"/>, and returns the ID of the created category.
/// </remarks>
/// <param name="repository">The category repository used to persist the new category.</param>
/// <param name="validator">The validator for the create category request.</param>
internal sealed class CreateCategoryHandler(
    ICategoryRepository repository,
    IValidator<CreateCategoryRequest> validator)
    : ICreateCategoryHandler
{
    /// <summary>
    /// Handles the create category request asynchronously.
    /// </summary>
    /// <param name="request">The create category request containing the data for the new category.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A <see cref="Result{T}"/> containing the ID of the created category if successful,
    /// or validation errors if the request is invalid.
    /// </returns>
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
                    .AsErrors()
            );
        }

        var title = new Title(request.Title);
        var category = new Category(title);
        category.ChangeDescription(request.Description);

        await repository.AddAsync(category, cancellationToken);
        _ = await repository
            .UnitOfWork
            .SaveChangesAsync(cancellationToken);

        return Result<long>.Created(category.Id);
    }
}
