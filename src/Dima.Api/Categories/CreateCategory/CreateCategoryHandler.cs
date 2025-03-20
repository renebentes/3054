using Dima.Api.Common;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

/// <summary>
/// Handles the creation of a new category.
/// </summary>
/// <param name="repository">The category repository.</param>
/// <param name="unitOfWork">The unit of work.</param>
/// <param name="validator">The validator for the create category request.</param>
internal sealed class CreateCategoryHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
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
                    .AsErrors()
            );
        }

        var title = new Title(request.Title);
        var category = new Category(title);
        category.ChangeDescription(request.Description);

        await repository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<long>.Created(category.Id);
    }
}
