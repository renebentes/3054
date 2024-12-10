namespace Dima.Core.Primitives;

/// <summary>
/// Represents a domain error object.
/// </summary>
/// <param name="Code">The error code</param>
/// <param name="Message">The error message</param>
public sealed record Error(string Code, string Message)
{
    /// <summary>
    /// Converts an <see cref="Error"/> object into its corresponding <see cref="Code"/> string
    /// </summary>
    /// <param name="error">The domain error object</param>
    /// <returns>Returns its <see cref="Code"/> string converted.</returns>
    public static implicit operator string(Error error)
        => error.Code;
}
