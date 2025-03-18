namespace Dima.Core.Categories.CreateCategory;

/// <summary>
/// Represents a create category request
/// </summary>
/// <param name="Title">Gets the <see cref="Category"/> title</param>
/// <param name="Description">Gets the <see cref="Category"/> description</param>
public sealed record CreateCategoryRequest(string Title, string Description)
    : IRequest<Result<long>>;
