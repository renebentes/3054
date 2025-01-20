using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using Dima.Core.Primitives.Result;

namespace Dima.Api.Categories.CreateCategory;

internal sealed class CreateCategoryHandler(DimaDbContext context)
    : ICreateCategoryHandler
{
    public async Task<Result> HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var title = new Title(request.Title);
        var category = new Category(title);
        category.SetDescription(request.Description);

        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
