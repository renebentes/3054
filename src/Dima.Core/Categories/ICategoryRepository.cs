namespace Dima.Core.Categories;

/// <summary>
/// Defines a contract for a repository that manages <see cref="Category"/> entities.
/// </summary>
/// <remarks>
/// Provides methods to interact with the category data store, including adding new categories.
/// Inherits from <see cref="IRepository"/> to support unit of work and transaction management.
/// </remarks>
public interface ICategoryRepository : IRepository
{
    /// <summary>
    /// Asynchronously adds a new <see cref="Category"/> to the data store.
    /// </summary>
    /// <param name="category">The <see cref="Category"/> entity to add.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous add operation.
    /// </returns>
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
}
