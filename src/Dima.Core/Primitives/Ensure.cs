namespace Dima.Core.Primitives;

/// <summary>
/// Contains assertions for the most common application checks.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Ensures that specified <see langword="string"/> <paramref name="input"/> is not longer than <paramref name="maxLength"/>
    /// </summary>
    /// <param name="input">The input to check</param>
    /// <param name="maxLength">The maximum length</param>
    /// <param name="error">The <see cref="Error"/> object to display on failure.</param>
    /// <exception cref="DomainException">Thrown if <paramref name="input"/> is longer than <paramref name="maxLength"/></exception>
    public static void MaxLength(string input, int maxLength, Error error)
    {
        if (input.Length > maxLength)
        {
            throw new DomainException(error);
        }
    }

    /// <summary>
    /// Ensures that the specified <see cref="string"/> <paramref name="input"/> is not empty.
    /// </summary>
    /// <param name="input">The input to check</param>
    /// <param name="error">The <see cref="Error"/> object to display on failure.</param>
    /// <exception cref="DomainException">Thrown if <paramref name="input"/> is <see langword="null"/> or empty.</exception>
    public static void NotEmpty(string input, Error error)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new DomainException(error);
        }
    }
}
