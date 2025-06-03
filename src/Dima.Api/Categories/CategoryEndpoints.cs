using Dima.Api.Categories.CreateCategory;
using Dima.Api.Categories.UpdateCategory;

namespace Dima.Api.Categories;

/// <summary>
/// Configures the category-related API endpoints for the application.
/// </summary>
/// <remarks>
/// This method maps the category-related endpoints under the route <c>/v1/categories</c>
/// and applies the "Categories" tag to them.
/// </remarks>
internal static class CategoryEndpoints
{
    /// <summary>
    /// Configures the application's HTTP endpoints for category-related operations.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to which the category endpoints will be added.</param>
    /// <returns>The <see cref="WebApplication"/> instance with the category endpoints configured.</returns>
    internal static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGroup("/v1/categories")
            .WithTags("Categories")
            .MapCreateCategoryEndpoint()
            .MapUpdateCategoryEndpoint();

        return app;
    }
}
