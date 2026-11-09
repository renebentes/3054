using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Dima.UnitTests.Api.Configuring;

public sealed class DatabaseFixture
{
    public async Task<DimaDbContext> CreateContextAsync(
        CancellationToken cancellationToken)
    {
        DbContextOptions<DimaDbContext> options = new DbContextOptionsBuilder<DimaDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new DimaDbContext(options);

        try
        {
            await context.Database.OpenConnectionAsync(cancellationToken);
            await context.Database.EnsureCreatedAsync(cancellationToken);

            return context;
        }
        catch
        {
            await context.DisposeAsync();
            throw;
        }
    }
}
