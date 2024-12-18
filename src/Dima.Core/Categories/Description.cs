namespace Dima.Core.Categories;

/// <summary>
/// Represents a <see cref="Description"/> for a <see cref="Category"/> object.
/// </summary>
/// <param name="Text">The description text</param>
public record Description(string Text)
{
    /// <summary>
    /// The description maximum length
    /// </summary>
    public const int MaxLength = 255;

    /// <summary>
    /// Gets the description text
    /// </summary>
    public string Text { get; init; } = Validate(Text);

    /// <summary>
    /// Validates the <see cref="Description"/> text
    /// </summary>
    /// <param name="text">The text to validate</param>
    /// <returns>The valid text</returns>
    /// <exception cref="DomainException"></exception>
    private static string Validate(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            text = string.Empty;
        }

        Ensure.MaxLength(text, MaxLength, DomainErrors.Description.LongerThanAllowed);

        return text;
    }

    /// <summary>
    /// Implicitly converts a <see langword="string"/> <paramref name="text"/> to a <see cref="Description"/> object.
    /// </summary>
    /// <param name="text">The text to convert</param>
    public static implicit operator Description(string text)
        => new(text);

    /// <inheritdoc/>
    public override string ToString()
        => Text;
}
