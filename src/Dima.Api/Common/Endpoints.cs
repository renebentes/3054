using Dima.Api.Categories;

namespace Dima.Api.Common;

internal static class Endpoints
{
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
