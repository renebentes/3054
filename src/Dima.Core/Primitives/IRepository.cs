namespace Dima.Core.Primitives;

/// <summary>
/// Defines a contract for a repository that provides access to a collection of entities and is associated with a unit of work.
/// </summary>
/// <remarks>
/// This interface should be implemented by repositories that manage entities and coordinate changes
/// through a unit of work. Implementations are expected to provide methods for querying and manipulating
/// entities, ensuring that all changes are tracked and persisted as part of the associated <see cref="IUnitOfWork"/>.
/// </remarks>
public interface IRepository
{
    /// <summary>
    /// Gets the current <see cref="IUnitOfWork"/> instance used to manage database transactions and operations.
    /// </summary>
    public IUnitOfWork UnitOfWork { get; }
}
