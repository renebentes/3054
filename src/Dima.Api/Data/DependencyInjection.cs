using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

/// <summary>
/// Contains extension methods for registering persistence and repository services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures persistence-related services required by the application.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to which persistence services will be added.</param>
    /// <returns>The same <see cref="IHostApplicationBuilder"/> instance to allow call chaining.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the connection string with the key <c>DimaDb</c> cannot be found in the application's configuration.
    /// </exception>
    /// <remarks>
    /// This method:
    /// <list type="bullet">
    /// <item>Reads the connection string using the configuration key <c>DimaDb</c>.</item>
    /// <item>Registers the application's EF Core <c>DbContext</c> for PostgreSQL via <c>AddNpgsqlDbContext</c>.</item>
    /// <item>Registers repository services by invoking <see cref="AddRepositories"/>.</item>
    /// </list>
    /// </remarks>
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DimaDb))
                               ?? throw new InvalidOperationException($"Connection string '{nameof(ConnectionStrings.DimaDb)}' not found.");
        builder.Services
            .AddSingleton(new ConnectionStrings(connectionString));

        builder.AddNpgsqlDbContext<DimaDbContext>(nameof(ConnectionStrings.DimaDb));
        builder.EnrichNpgsqlDbContext<DimaDbContext>();

        return builder;
    }
}
