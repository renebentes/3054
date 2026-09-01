namespace Dima.Core.Categories.DeleteCategory;

/// <summary>
/// Represents a command to delete a category by its identifier.
/// </summary>
/// <param name="Id">The identifier of the category to be deleted.</param>
public sealed record DeleteCategoryRequest(long Id)
    : IRequest<Result>;
