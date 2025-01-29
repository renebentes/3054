namespace Dima.Core.Categories;

/// <summary>
/// Contains the validation errors.
/// </summary>
internal static class ValidationErrors
{
    /// <summary>
    /// Contains the create category errors.
    /// </summary>
    internal static class CreateCategory
    {
        internal static Error TitleIsRequired
            => new(
                "CreateCategory.TitleIsRequired",
                "The title is required."
            );
    }
}
