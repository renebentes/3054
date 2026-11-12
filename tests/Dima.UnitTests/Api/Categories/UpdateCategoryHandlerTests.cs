using Dima.Api.Categories.UpdateCategory;
using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Dima.UnitTests.Api.Categories;

public class UpdateCategoryHandlerTests
{
    [Fact]
    public async Task HandleAsync_ShouldReturnInvalid_WhenValidationFails()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "", "Desc");
        var cancellationToken = TestContext.Current.CancellationToken;
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
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
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
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
        await using DimaDbContext context = await DimaDbContextFactory.CreateAsync(cancellationToken);
        var title = new Title("Old Title");
        var description = new Description("Old Desc");
        var category = new Category(title);
        category.ChangeDescription(description);

        context
            .Categories
            .Add(category);
        await context.SaveChangesAsync(cancellationToken);

        var titleUpdated = new Title("New Title");
        var descriptionUpdated = new Description("New Desc");
        var request = new UpdateCategoryRequest(
            category.Id,
            titleUpdated,
            descriptionUpdated);
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
               x => x.Id == request.Id,
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
