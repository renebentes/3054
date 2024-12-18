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
        Ensure.NullOrEmpty(value, DomainErrors.Title.NullOrEmpty);
        Ensure.MaxLength(value, MaxLength, DomainErrors.Title.LongerThanAllowed);

        return value;
    }
}
