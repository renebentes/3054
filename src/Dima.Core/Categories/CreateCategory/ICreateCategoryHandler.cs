using Dima.Core.Messaging;

namespace Dima.Core.Categories.CreateCategory;

public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Result<long>>
{
}
