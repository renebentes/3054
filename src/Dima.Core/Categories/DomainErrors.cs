namespace Dima.Core.Categories;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static partial class DomainErrors
{

    /// <summary>
    /// Contains the category errors.
    /// </summary>
    public static class Category
    {
        public static Error NotFound => new(
            "Category.NotFound",
            "The category with the specified identifier was not found.");
    }
    /// <summary>
    /// Contains the description errors/>
    /// </summary>
    public static class Description
    {
        public static Error LongerThanAllowed => new("Description.LongerThanAllowed", "The description is longer than allowed.");
    }

    /// <summary>
    /// Contains the title errors/>
    /// </summary>
    public static class Title
    {
        public static Error LongerThanAllowed => new("Title.LongerThanAllowed", "The title is longer than allowed.");

        public static Error NullOrEmpty => new("Title.NullOrEmpty", "The title is required");
    }
}
