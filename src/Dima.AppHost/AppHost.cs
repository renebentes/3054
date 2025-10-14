IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> database = builder
    .AddPostgres("postgres")
    .WithPgAdmin()
    .WithImage("postgres:17")
    .AddDatabase("DimaDb");

IResourceBuilder<ProjectResource> migrations = builder
    .AddProject<Projects.Dima_Services_Migrations>("dima-migrations")
    .WithEnvironment("ConnectionStrings__DimaDb", database)
    .WithReference(database)
    .WaitFor(database);

builder
    .AddProject<Projects.Dima_Api>("dima-api")
    .WithEnvironment("ConnectionStrings__DimaDb", database)
    .WithReference(database)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

await builder
    .Build()
    .RunAsync();
