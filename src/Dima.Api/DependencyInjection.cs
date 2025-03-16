using Dima.Api.Categories.CreateCategory;
using Dima.Core.Categories.CreateCategory;

namespace Dima.Api;

/// <summary>
/// Provides extension methods for adding API services to the dependency injection container.
/// </summary>
internal static class DependencyInjection
{
    /// <summary>
    /// Adds API services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    internal static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddDocumentation();

        services.AddTransient<ICreateCategoryHandler, CreateCategoryHandler>();

        return services;
    }

    /// <summary>
    /// Adds documentation services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    private static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}
