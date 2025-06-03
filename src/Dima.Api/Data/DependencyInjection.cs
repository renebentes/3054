using Dima.Api.Data.Categories;
using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

/// <summary>
/// Contains the dependency injection extensions.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the persistence data.
    /// </summary>
    /// <param name="services">The collection of service descriptors.</param>
    /// <param name="configuration">The application configuration properties.</param>
    /// <returns>Returns the updated service collection.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionString.DefaultConnection)!;
        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<DimaDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddRepositories();
        return services;
    }

    /// <summary>
    /// Registers repositories with the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the repository services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
