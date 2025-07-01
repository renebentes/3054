using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data.Categories;

/// <inheritdoc cref="ICategoryRepository" />
public class CategoryRepository(DimaDbContext context)
    : ICategoryRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
        => await context
            .Categories
            .AddAsync(
                category,
                cancellationToken
            );

    public async Task<Category?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await context
            .Categories
            .SingleOrDefaultAsync(
                c => c.Id == id,
                cancellationToken
            );

    public void Update(Category category)
        => context
            .Categories
            .Update(category);
}
