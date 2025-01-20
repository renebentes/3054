namespace Dima.Api.Data;

/// <summary>
/// Represents a connection string.
/// </summary>
/// <param name="Value">The actual connection string value</param>
public sealed record ConnectionString(string Value)
{
    /// <summary>
    /// The connection string key for the default connection.
    /// </summary>
    public const string DefaultConnection = nameof(DefaultConnection);

    /// <summary>
    /// Implicitly converts a <see cref="ConnectionString"/> to a <see cref="string"/>.
    /// </summary>
    /// <param name="connectionString">The <see cref="ConnectionString"/> object</param>
    public static implicit operator string(ConnectionString connectionString)
        => connectionString.Value;
}
