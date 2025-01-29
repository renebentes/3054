using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using Dima.Core.Primitives;
using Dima.Core.Primitives.Result;
using FluentValidation;

namespace Dima.Api.Categories.CreateCategory;

internal sealed class CreateCategoryHandler(
    DimaDbContext context,
    IValidator<CreateCategoryRequest> validator
    ) : ICreateCategoryHandler
{
    public async Task<Result> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validatorResult.IsValid)
        {
            return Result.Invalid(
                new Error(
                    "CreateCategory.Validation",
                    validatorResult.ToString()
                )
            );
        }

        var title = new Title(request.Title);
        var category = new Category(title);
        category.SetDescription(request.Description);

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
