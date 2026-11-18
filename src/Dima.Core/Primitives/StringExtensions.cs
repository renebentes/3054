namespace Dima.Core.Primitives;

/// <summary>
/// Provides normalization operations for strings.
/// </summary>
public static class StringExtensions
{
    /// <param name="value">The string to normalize.</param>
    extension(string value)
    {
        /// <summary>
        /// Removes whitespace from the beginning and end of a string.
        /// </summary>
        /// <returns>The string without leading or trailing whitespace.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        public string TrimWhitespace()
        {
            ArgumentNullException.ThrowIfNull(value);
            return value.Trim();
        }
    }

}
