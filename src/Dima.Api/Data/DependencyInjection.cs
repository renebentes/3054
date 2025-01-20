using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionString.DefaultConnection)!;
        services.AddSingleton(new ConnectionString(connectionString));

        services.AddDbContext<DimaDbContext>(options =>
            options.UseNpgsql(connectionString));
        return services;
    }
}
