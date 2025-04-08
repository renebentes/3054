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

    public async Task<Category> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
#pragma warning disable CS8603 // Possible null reference return.
        => await context
        .Categories
        .FindAsync(
            [id],
            cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.

    public void Update(Category category)
        => context.Categories.Update(category);
}
