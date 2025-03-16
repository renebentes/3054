using Dima.Api.Categories.CreateCategory;

namespace Dima.Api.Categories;

internal static class CategoryEndpoints
{
    internal static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/categories")
            .WithTags("Categories")
            .MapCreateCategoryEndpoint();

        return app;
    }
}
