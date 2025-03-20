
using Dima.Core.Categories;

namespace Dima.Api.Data;

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
}
