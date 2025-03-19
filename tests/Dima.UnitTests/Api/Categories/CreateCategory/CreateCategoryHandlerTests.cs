using Dima.Api.Categories.CreateCategory;
using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Dima.UnitTests.Api.Categories.CreateCategory;

public sealed class CreateCategoryHandlerTests
    : IDisposable
{
    private readonly DimaDbContext _context;
    private readonly CreateCategoryHandler _handler;
    private readonly IValidator<CreateCategoryRequest> _validator;
    private readonly DbConnection _connection;

    public CreateCategoryHandlerTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<DimaDbContext>()
            .UseSqlite(Guid.NewGuid().ToString())
            .Options;

        _context = Substitute.For<DimaDbContext>(options);
        _validator = new CreateCategoryRequestValidator();
        _handler = new CreateCategoryHandler(_context, _validator);
    }

    public void Dispose()
        => _connection.Dispose();

    [Theory]
    [InlineData("test title", "")]
    [InlineData("test title", " ")]
    [InlineData("test title", null)]
    [InlineData("test title", "test description")]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid(
        string title,
        string? description)
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            title,
            description!);

        // Act
        var result = await _handler.HandleAsync(
            createCategoryRequest,
            CancellationToken.None);

        // Assert
        await _context
            .Received(1)
            .AddAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
        await _context
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());

        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Status
            .Should()
            .Be(ResultStatus.Created);
    }

    [Theory]
    [InlineData("", "test description")]
    [InlineData(" ", "test description")]
    [InlineData(null, "test description")]
    public async Task HandleAsync_ShouldReturnInvalid_WhenRequestIsInvalid(
        string? title,
        string description)
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            title!,
            description);

        // Act
        var result = await _handler.HandleAsync(
            createCategoryRequest,
            CancellationToken.None);

        // Assert
        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Status
            .Should()
            .Be(ResultStatus.Invalid);
    }
}
