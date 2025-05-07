using Dima.Core.Categories;

namespace Dima.Api.Data.Categories;

/// <inheritdoc cref="ICategoryRepository"/>
public class CategoryRepository(DimaDbContext context)
    : ICategoryRepository
{
    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default)
        => await context
        .Categories
        .AddAsync(
            category,
            cancellationToken);

    public async Task<Category?> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
        => await context
        .Categories
        .Where(c => c.Id == id)
        .SingleOrDefaultAsync(cancellationToken);

    public void Update(Category category)
        => context.Categories.Update(category);
}
