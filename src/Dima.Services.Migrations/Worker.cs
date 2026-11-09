using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Dima.Services.Migrations;

internal class Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime)
    : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using Activity? activity = _activitySource.StartActivity(
            "Initializing database",
            ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope
                .ServiceProvider
                .GetRequiredService<DimaDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private async Task RunMigrationAsync(
        DimaDbContext context,
        CancellationToken cancellationToken = default)
    {
        var stopWatch = Stopwatch.StartNew();

        var strategy = context
            .Database
            .CreateExecutionStrategy();

        await strategy.ExecuteAsync(
            context.Database.MigrateAsync,
            cancellationToken);

        logger.LogInformation(
            "Database initialization completed after {ElapsedMilliseconds}ms",
            stopWatch.ElapsedMilliseconds);
    }
}
