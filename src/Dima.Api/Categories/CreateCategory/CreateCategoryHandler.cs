using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

internal sealed class CreateCategoryHandler(
    DimaDbContext context,
    IValidator<CreateCategoryRequest> validator)
    : ICreateCategoryHandler
{
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
        category.SetDescription(request.Description);

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result<long>.Created(category.Id);
    }
}
