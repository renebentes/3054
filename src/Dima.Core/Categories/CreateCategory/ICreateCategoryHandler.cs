using Dima.Core.Messaging;
using Dima.Core.Primitives.Result;

namespace Dima.Core.Categories.CreateCategory;

public interface ICreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Result>
{
}
