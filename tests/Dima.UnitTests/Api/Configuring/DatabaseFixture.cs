using Dima.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Dima.UnitTests.Api.Configuring;

public sealed class DatabaseFixture
    : IDisposable
{
    private readonly DbConnection _connection;

    public DatabaseFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        using DimaDbContext context = CreateContext();
        context.Database.EnsureCreated();
    }

    public DimaDbContext CreateContext()
        => new(new DbContextOptionsBuilder<DimaDbContext>()
            .UseSqlite(_connection)
            .Options);

    public void Dispose()
    {
        _connection.Dispose();
    }
}
