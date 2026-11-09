using Dima.Api.Categories.UpdateCategory;
using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Dima.UnitTests.Api.Categories;

public class UpdateCategoryHandlerTests
    (DatabaseFixture fixture)
    : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task HandleAsync_ShouldReturnInvalid_WhenValidationFails()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "", "Desc");
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await fixture.CreateContextAsync(cancellationToken);
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(request, cancellationToken);

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

    [Fact]
    public async Task HandleAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "Test", "Desc");
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await fixture.CreateContextAsync(cancellationToken);
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(request, cancellationToken);

        // Assert
        result
            .ShouldNotBeNull();

        result
            .IsSuccess
            .ShouldBeFalse();

        result
            .Status
            .ShouldBe(ResultStatus.NotFound);
    }

    [Fact]
    public async Task HandleAsync_ShouldUpdateCategory_WhenRequestIsValid()
    {
        // Arrange
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await fixture.CreateContextAsync(cancellationToken);
        var category = new Category("Old Title");
        category.ChangeDescription("Old Desc");

        context
            .Categories
            .Add(category);
        await context.SaveChangesAsync(cancellationToken);

        var request = new UpdateCategoryRequest(category.Id, "New Title", "New Desc");
        var handler = CreateHandler(context);

        // Act
        var result = await handler.HandleAsync(request, cancellationToken);

        // Assert
        result.ShouldNotBeNull();

        result
            .IsSuccess
            .ShouldBeTrue();

        result
            .Status
            .ShouldBe(ResultStatus.NoContent);

        var updatedCategory = await context.Categories
           .AsNoTracking()
           .FirstOrDefaultAsync(
               x => x.Id == category.Id,
               cancellationToken);

        updatedCategory.ShouldNotBeNull();
        updatedCategory
            .Title
            .ToString()
            .ShouldBe(request.Title);
        updatedCategory
            .Description
            .ToString()
            .ShouldBe(request.Description);
    }

    private static UpdateCategoryHandler CreateHandler(DimaDbContext context)
    {
        IValidator<UpdateCategoryRequest> validator = new UpdateCategoryRequestValidator();
        return new UpdateCategoryHandler(
            context,
            validator);
    }
}
