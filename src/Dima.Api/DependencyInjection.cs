using Dima.Api.Categories.CreateCategory;
using Dima.Api.Categories.UpdateCategory;
using Dima.Core.Categories.CreateCategory;
using Dima.Core.Categories.UpdateCategory;

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
        services
            .AddDocumentation()
            .AddHandlers();

        return services;
    }

    /// <summary>
    /// Adds documentation services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added services.</returns>
    private static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Registers handler implementations for operations in the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the handlers will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered handlers.</returns>
    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<ICreateCategoryHandler, CreateCategoryHandler>();
        services.AddTransient<IUpdateCategoryHandler, UpdateCategoryHandler>();

        return services;
    }
}
