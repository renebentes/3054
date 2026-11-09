using Dima.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Dima.UnitTests.Api.Configuring;

public sealed class DatabaseFixture
    : IAsyncLifetime
{
    private DbConnection _connection = null!;

    public DimaDbContext CreateContext()
    {
        DbContextOptions<DimaDbContext> options = new DbContextOptionsBuilder<DimaDbContext>()
            .UseSqlite(_connection)
            .Options;

        return new(options);
    }

    public async ValueTask DisposeAsync()
        => await _connection.DisposeAsync();

    public async ValueTask InitializeAsync()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

        await using DimaDbContext context = CreateContext();
        await context.Database.EnsureCreatedAsync();
    }
}
