namespace Dima.Core.Primitives;

/// <summary>
/// Represents a repository that provides access to a collection of entities and is associated with a unit of work.
/// </summary>
/// <remarks>This interface defines a contract for repositories that manage entities and coordinate changes 
/// through a unit of work. Implementations of this interface should provide methods for querying and manipulating
/// entities while ensuring that changes are tracked and persisted as part of the associated <see
/// cref="IUnitOfWork"/>.</remarks>
public interface IRepository
{
    /// <summary>
    /// Gets the current unit of work instance used to manage database transactions and operations.
    /// </summary>
    public IUnitOfWork UnitOfWork { get; }
}
