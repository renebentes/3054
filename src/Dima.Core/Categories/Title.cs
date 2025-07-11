namespace Dima.Core.Categories;

/// <summary>
/// Represents a <see cref="Title"/> for a <see cref="Category"/> object.
/// </summary>
/// <param name="Value">The title value</param>
public sealed record Title(string Value)
{
    /// <summary>
    /// The title maximum length
    /// </summary>
    public const int MaxLength = 80;

    /// <summary>
    /// Gets the title value
    /// </summary>
    public string Value { get; init; } = Validate(Value);

    /// <summary>
    /// Validates the <see cref="Title"/> value
    /// </summary>
    /// <param name="value">The value to validate</param>
    /// <returns>The valid value</returns>
    /// <exception cref="DomainException"></exception>
    private static string Validate(string value)
    {
        Ensure.NotEmpty(value, DomainErrors.Title.NullOrEmpty);
        Ensure.MaxLength(value, MaxLength, DomainErrors.Title.LongerThanAllowed);

        return value;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Title"/> object to a <see langword="string"/>.
    /// </summary>
    /// <param name="title">The text to convert</param>
    public static implicit operator string(Title title)
        => title.ToString();

    /// <summary>
    /// Implicitly converts a <see langword="string"/> to a <see cref="Title"/> object.
    /// </summary>
    /// <param name="value">The string value to convert</param>
    public static implicit operator Title(string value)
        => new(value);

    /// <inheritdoc/>
    public override string ToString()
        => Value;
}
