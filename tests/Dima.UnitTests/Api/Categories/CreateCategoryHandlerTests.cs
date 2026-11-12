using Dima.Api.Categories.CreateCategory;
using Dima.Api.Data;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateCategoryRequest(
            "Category title",
            "Category description");
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(
            request,
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

        var categoryCreated = await context
            .Categories
            .AsNoTracking()
            .SingleOrDefaultAsync(
                c => c.Title.Value == request.Title,
                cancellationToken);

        categoryCreated
            .ShouldNotBeNull();

        categoryCreated
            .Description
            .ToString()
            .ShouldBe(request.Description);
    }

    [Fact]
    public async Task HandleAsync_ShouldCreateCategoryWithEmptyDescription_WhenDescriptionIsNull()
    {
        var request = new CreateCategoryRequest("Category title", null);
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
        var handler = CreateHandler(context);

        var result = await handler.HandleAsync(request, cancellationToken);

        result
            .Status
            .ShouldBe(ResultStatus.Created);

        var categoryCreated = context.Categories.Local
            .Single(c => c.Title.Value == request.Title);

        categoryCreated.Description.ShouldBe(Dima.Core.Categories.Description.Empty);
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
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
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
