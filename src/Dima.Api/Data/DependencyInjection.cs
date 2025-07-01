using Dima.Api.Data.Categories;
using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

/// <summary>
/// Contains extension methods for registering persistence and repository services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds persistence-related services to the specified <see cref="IServiceCollection"/>, including the database context and repositories.
    /// </summary>
    /// <param name="services">The collection of service descriptors to which persistence services will be added.</param>
    /// <param name="configuration">The application configuration properties used to retrieve the connection string.</param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with persistence services registered.
    /// </returns>
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
    /// Registers repository services in the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The collection of service descriptors to which repository services will be added.</param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> with repository services registered.
    /// </returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
