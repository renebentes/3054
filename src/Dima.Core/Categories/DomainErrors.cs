namespace Dima.Core.Categories;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static partial class DomainErrors
{
    /// <summary>
    /// Contains the title errors/>
    /// </summary>
    public static class Title
    {
        public static Error LongerThanAllowed => new("Title.LongerThanAllowed", "The title is longer than allowed.");

        public static Error NullOrEmpty => new("Title.NullOrEmpty", "The title is required");
    }
}
