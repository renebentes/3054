namespace Dima.Core.Categories.DeleteCategory;

/// <summary>
/// Defines a handler for processing delete category requests.
/// </summary>
/// <remarks>
/// Handles <see cref="DeleteCategoryRequest"/> and returns a <see cref="Result"/> indicating the outcome.
/// </remarks>
public interface IDeleteCategoryHandler
    : IRequestHandler<DeleteCategoryRequest, Result>;
