namespace Dima.Core.Categories;

/// <summary>
/// <para>Interface for the Category repository.</para>
/// <para>Provides methods to interact with the Category data store.</para>
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    /// Asynchronously adds a new category to the data store.
    /// </summary>
    /// <param name="category">The category to add.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
}
