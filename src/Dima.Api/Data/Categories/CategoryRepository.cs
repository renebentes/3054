using Dima.Core.Categories;

namespace Dima.Api.Data.Categories;

/// <inheritdoc cref="ICategoryRepository" />
public class CategoryRepository(DimaDbContext context)
    : ICategoryRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        => await context.Categories.AddAsync(category, cancellationToken);
}
