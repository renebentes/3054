using Dima.Api;
using Dima.Api.Common;
using Dima.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddCore();

builder.AddPersistence();

builder.Services
    .AddApi();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapDocumentation();
}

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.MapEndpoints();

await app.RunAsync();
