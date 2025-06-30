namespace Dima.Core.Categories.UpdateCategory;

/// <summary>
/// Represents a request to update a category.
/// </summary>
/// <param name="Id">The unique identifier of the category.</param>
/// <param name="Title">The title of the category.</param>
/// <param name="Description">The description of the category.</param>
public sealed record UpdateCategoryRequest(long Id, string Title, string Description)
    : IRequest<Result>;
