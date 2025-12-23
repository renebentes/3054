namespace Dima.Core.Categories;

/// <summary>
/// Contains domain errors related to the <c>Category</c> entity.
/// Provides standardized error messages for common validation and operation scenarios.
/// </summary>
public static class CategoryErrors
{
    private const string ErrorPrefix = nameof(Category);

    /// <summary>
    /// Returns an error indicating that the category with the provided identifier was not found.
    /// </summary>
    /// <param name="id">The category identifier.</param>
    /// <returns>An error representing the absence of the category.</returns>
    public static Error NotFound(long id)
        => new(
        $"{ErrorPrefix}.{nameof(NotFound)}",
        $"The category with the identifier '{id}' was not found.");

    /// <summary>
    /// Groups errors related to the <c>Description</c> property of the category.
    /// </summary>
    public static class Description
    {
        /// <summary>
        /// Error indicating that the description exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed
            => new(
            $"{ErrorPrefix}.{nameof(Description)}.{nameof(LongerThanAllowed)}",
            "The description is longer than allowed.");
    }

    /// <summary>
    /// Groups errors related to the <c>Title</c> property of the category.
    /// </summary>
    public static class Title
    {
        /// <summary>
        /// Error indicating that the title exceeds the maximum allowed length.
        /// </summary>
        public static Error LongerThanAllowed
            => new(
            $"{ErrorPrefix}.{nameof(Title)}.{nameof(LongerThanAllowed)}",
            "The title is longer than allowed.");

        /// <summary>
        /// Error indicating that the title is null or empty.
        /// </summary>
        public static Error NullOrEmpty
            => new(
            $"{ErrorPrefix}.{nameof(Title)}.{nameof(NullOrEmpty)}",
            "The title is required");
    }

    /// <summary>
    /// Groups errors related to the <c>UserId</c> property of the category.
    /// </summary>
    public static class UserId
    {
        /// <summary>
        /// Error indicating that the user identifier is null or empty.
        /// </summary>
        public static Error NullOrEmpty
            => new(
            $"{ErrorPrefix}.{nameof(UserId)}.{nameof(NullOrEmpty)}",
            "The user identifier is required.");

        /// <summary>
        /// Returns an error indicating that the user identifier exceeds the maximum allowed length.
        /// </summary>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <returns>An error representing the user identifier length violation.</returns>
        public static Error LongerThanAllowed(int maxLength)
            => new(
            $"{ErrorPrefix}.{nameof(UserId)}.{nameof(LongerThanAllowed)}",
            $"The UserId is longer than the allowed '{maxLength}' characters.");
    }
}
