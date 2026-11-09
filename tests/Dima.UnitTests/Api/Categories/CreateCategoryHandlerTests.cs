using Dima.Api.Categories.CreateCategory;
using Dima.Api.Data;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
    (DatabaseFixture fixture)
    : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            "Category title",
            "Category description");
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await fixture.CreateContextAsync(cancellationToken);
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(
            createCategoryRequest,
            cancellationToken);

        // Assert
        result
            .ShouldNotBeNull();

        result
            .IsSuccess
            .ShouldBeTrue();

        result
            .Status
            .ShouldBe(ResultStatus.Created);
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
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await fixture.CreateContextAsync(cancellationToken);
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(
            createCategoryRequest,
            cancellationToken);

        // Assert
        result
            .ShouldNotBeNull();

        result
            .IsSuccess
            .ShouldBeFalse();

        result
            .Status
            .ShouldBe(ResultStatus.Invalid);
    }

    private static CreateCategoryHandler CreateHandler(DimaDbContext context)
    {
        IValidator<CreateCategoryRequest> validator = new CreateCategoryRequestValidator();
        return new CreateCategoryHandler(
            context,
            validator);
    }
}
