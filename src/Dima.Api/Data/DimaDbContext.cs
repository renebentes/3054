using Dima.Core.Categories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dima.Api.Data;

/// <summary>
/// Represents the database context for the Dima application.
/// </summary>
/// <param name="options">The database context options</param>
public class DimaDbContext(DbContextOptions<DimaDbContext> options)
    : DbContext(options),
    IUnitOfWork
{
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
