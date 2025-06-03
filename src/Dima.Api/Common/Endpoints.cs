using Dima.Api.Categories;

namespace Dima.Api.Common;

/// <summary>
/// Provides extension methods for mapping API endpoints to a <see cref="WebApplication"/> instance.
/// </summary>
internal static class Endpoints
{
    /// <summary>
    /// Configures the application's HTTP endpoints, including health checks.
    /// </summary>
    /// <remarks>
    /// This method sets up the application's routing by mapping various endpoint groups:
    /// <list type="bullet">
    /// <item> <description>A health check endpoint at the root path (<c>/</c>) that returns a success
    /// message.</description> </item>
    /// <item> <description>Category-related endpoints, mapped via <see cref="MapCategoryEndpoints"/>.</description> </item>
    /// </list></remarks>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    /// <returns>The configured <see cref="WebApplication"/> instance.</returns>
    internal static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("");

        app.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/",
                () => Results.Ok("Dima API is running."));

        app.MapCategoryEndpoints();

        return app;
    }
}
