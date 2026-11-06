using Dima.Api.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Dima.UnitTests.Api.Configuring;

public class DatabaseFixture
    : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<DimaDbContext> _contextOptions;

    public DatabaseFixture()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<DimaDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new DimaDbContext(_contextOptions);
    }

    public DimaDbContext CreateContext()
        => new(_contextOptions);

    public void Dispose()
        => _connection.Dispose();
}
