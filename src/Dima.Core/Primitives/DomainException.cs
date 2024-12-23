namespace Dima.Core.Primitives;

/// <summary>
/// Represents an error that occurs if a domain business rule is not satisfied.
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="error">The error containing the information about what happened.</param>
    public DomainException(Error error)
        : base(error.Message)
        => Error = error;

    /// <summary>
    /// Gets the domain error.
    /// </summary>
    public Error Error { get; }
}
