namespace Dima.Core.Categories.CreateCategory;

/// <summary>
/// Defines a handler for creating a category.
/// </summary>
public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Result<long>>;
