using Dima.Api.Data;
using Dima.Services.Migrations;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services
    .AddHostedService<Worker>();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.AddNpgsqlDbContext<DimaDbContext>(nameof(ConnectionStrings.DimaDb));

var host = builder.Build();
await host.RunAsync();
