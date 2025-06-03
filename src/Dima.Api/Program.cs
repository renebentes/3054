using Dima.Api;
using Dima.Api.Common;
using Dima.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddPersistence(builder.Configuration)
    .AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

await app.RunAsync();
