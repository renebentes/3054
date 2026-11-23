using Aspire.Hosting.Dotnet;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> database = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithImage("postgres:17")
    .AddDatabase("DimaDb");

IResourceBuilder<DotnetProjectResource> migrations;
migrations = builder
    .AddDotnetProject("dima-migrations", "../Dima.Services.Migrations/Dima.Services.Migrations.csproj")
    .WithEnvironment("ConnectionStrings__DimaDb", database)
    .WithReference(database)
    .WaitFor(database);

builder
    .AddDotnetProject("dima-api", "../Dima.Api/Dima.Api.csproj")
    .WithEnvironment("ConnectionStrings__DimaDb", database)
    .WithReference(database)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

await builder
    .Build()
    .RunAsync();
