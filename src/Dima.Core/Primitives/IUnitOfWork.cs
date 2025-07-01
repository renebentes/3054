namespace Dima.Core.Primitives;

/// <summary>
/// Represents a unit of work that encapsulates a set of operations to be committed as a single transaction.
/// </summary>
/// <remarks>
/// This interface is typically used to coordinate changes across multiple repositories or data sources,
/// ensuring that all changes are saved atomically. Implementations should handle transaction management
/// and ensure data consistency.
/// </remarks>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Asynchronously saves all changes made in the current context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the asynchronous save operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous save operation. The task result contains
    /// the number of state entries written to the database.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
