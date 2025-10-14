using Dima.Api.Data;
using Dima.MigrationService;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services
    .AddHostedService<Worker>();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<DimaDbContext>(nameof(ConnectionStrings.DimaDb));

var host = builder.Build();
host.Run();
