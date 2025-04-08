namespace Dima.Core.Categories;

/// <summary>
/// Provides methods to interact with the Category data store.
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

    /// <summary>
    /// Asynchronously retrieves a category by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the category to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the category if found; otherwise, null.</returns>
    Task<Category> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing category in the data store.
    /// </summary>
    /// <param name="category">The category to update.</param>
    void Update(Category category);
}
