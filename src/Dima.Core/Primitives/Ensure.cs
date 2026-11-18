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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="maxLength"/> is negative</exception>
    /// <exception cref="DomainException">Thrown if <paramref name="input"/> is longer than <paramref name="maxLength"/></exception>
    /// <returns>The <paramref name="input"/> if it is not longer than <paramref name="maxLength"/></returns>
    public static string MaxLength(string input, int maxLength, Error error)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentOutOfRangeException.ThrowIfNegative(maxLength);

        return input.Length > maxLength
            ? throw new DomainException(error)
            : input;
    }

    /// <summary>
    /// Ensures that the specified <see cref="string"/> <paramref name="input"/> is not empty.
    /// </summary>
    /// <param name="input">The input to check</param>
    /// <param name="error">The <see cref="Error"/> object to display on failure.</param>
    /// <exception cref="DomainException">Thrown if <paramref name="input"/> is <see langword="null"/> or empty.</exception>
    /// <returns>The <paramref name="input"/> if it is not empty.</returns>
    public static string Required(string input, Error error)
        => string.IsNullOrWhiteSpace(input)
            ? throw new DomainException(error)
            : input;
}
