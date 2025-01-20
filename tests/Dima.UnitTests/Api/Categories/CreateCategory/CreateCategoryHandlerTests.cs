using Dima.Api.Categories.CreateCategory;
using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using Microsoft.EntityFrameworkCore;

namespace Dima.UnitTests.Api.Categories.CreateCategory;

public class CreateCategoryHandlerTests
{
    private readonly DimaDbContext _context;
    private readonly CreateCategoryHandler _handler;

    public CreateCategoryHandlerTests()
    {
        _context = Substitute.For<DimaDbContext>(new DbContextOptions<DimaDbContext>());
        _handler = new CreateCategoryHandler(_context);
    }

    [Theory]
    [InlineData("test title", "")]
    [InlineData("test title", " ")]
    [InlineData("test title", null)]
    [InlineData("test title", "test description")]
    public async Task ReturnsSuccessWhenGivenValidTest(string title, string? description)
    {
        var createCategoryRequest = new CreateCategoryRequest(title, description!);

        var result = await _handler.HandleAsync(createCategoryRequest, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        await _context.Received(1)
            .AddAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
        await _context.Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
