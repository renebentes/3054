using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dima.Core;

/// <summary>
/// Contains the dependency injection extensions.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds core services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
