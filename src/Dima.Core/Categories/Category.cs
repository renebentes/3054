namespace Dima.Core.Categories;

/// <summary>
/// Represents a <see cref="Category"/> entity.
/// </summary>
public sealed class Category : Entity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    /// <param name="title">Category title</param>
    public Category(Title title)
        => ChangeTitle(title);

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    private Category()
    {
    }

    /// <summary>
    /// Gets the category description.
    /// </summary>
    public Description Description { get; private set; } = default!;

    /// <summary>
    /// Gets the category title.
    /// </summary>
    public Title Title { get; private set; } = default!;

    /// <summary>
    /// Sets the category description.
    /// </summary>
    /// <param name="description">The category description.</param>
    public void ChangeDescription(Description description)
        => Description = description;

    /// <summary>
    /// Changes the category title.
    /// </summary>
    /// <param name="title">The <see cref="Title"/>.</param>
    public void ChangeTitle(Title title)
    {
        Ensure.NotEmpty(title, DomainErrors.Title.NullOrEmpty);
        Title = title;
    }
}
