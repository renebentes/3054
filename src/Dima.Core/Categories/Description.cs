namespace Dima.Core.Categories;

/// <summary>
/// Represents a <see cref="Description"/> for a <see cref="Category"/> object.
/// </summary>
public sealed record Description
{
    /// <summary>
    /// The description maximum length
    /// </summary>
    public const int MaxLength = 255;

    /// <summary>
    /// Gets an empty description value.
    /// </summary>
    public static Description Empty => new(string.Empty);

    /// <summary>
    /// Initializes a new instance of the <see cref="Description"/> record.
    /// </summary>
    /// <param name="text">The description text</param>
    public Description(string text)
        => Text = text;

    /// <summary>
    /// Gets the description text
    /// </summary>
    public string Text
    {
        get;
        init => field = Validate(value);
    }

    /// <summary>
    /// Validates the <see cref="Description"/> text
    /// </summary>
    /// <param name="text">The text to validate</param>
    /// <returns>The valid text</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="text"/> is null.</exception>
    /// <exception cref="DomainException">Thrown when the description exceeds the maximum length.</exception>
    private static string Validate(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        var normalizedText = text.Trim();

        if (string.IsNullOrWhiteSpace(normalizedText))
        {
            return string.Empty;
        }

        Ensure.MaxLength(normalizedText, MaxLength, DomainErrors.Description.LongerThanAllowed);

        return normalizedText;
    }

    /// <summary>
    /// Explicitly converts a <see langword="string"/> <paramref name="text"/> to a <see cref="Description"/> object.
    /// </summary>
    /// <param name="text">The text to convert</param>
    public static explicit operator Description(string text)
        => new(text);

    /// <summary>
    /// Implicitly converts a <see cref="Description"/> object to a <see langword="string"/>.
    /// </summary>
    /// <param name="description">The description to convert</param>
    public static implicit operator string(Description description)
        => description.Text;

    /// <inheritdoc/>
    public override string ToString()
        => Text;
}
