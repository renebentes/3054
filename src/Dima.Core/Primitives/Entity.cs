namespace Dima.Core.Primitives;

public abstract class Entity

{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(long id)
    {
        Id = id;
    }

    public long Id { get; private init; }
}
