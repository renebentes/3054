IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> database = builder
    .AddPostgres("postgres")
    .WithImage("postgres:17")
    .AddDatabase("dima")
    .WithHealthCheck("/health");

builder
    .AddProject<Projects.Dima_Api>("dima-api")
    .WithEnvironment("ConnectionStrings__DefaultConnection", database)
    .WithReference(database)
    .WaitFor(database);

await builder
    .Build()
    .RunAsync();
