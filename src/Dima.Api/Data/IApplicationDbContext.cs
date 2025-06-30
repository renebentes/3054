using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

/// <summary>
/// Defines the application database context interface.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> of <see cref="Category"/> entities.
    /// </summary>
    public DbSet<Category> Categories { get; }

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
