using Dima.Api.Categories.DeleteCategory;
using Dima.Api.Data;
using Dima.Core.Categories.DeleteCategory;
using Dima.UnitTests.Api.Configuring;

namespace Dima.UnitTests.Api.Categories;

public sealed class DeleteCategoryHandlerTests
    (DatabaseFixture fixture)
    : IClassFixture<DatabaseFixture>
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task HandleAsync_ShouldReturnInvalid_WhenValidationFails(long id)
    {
        // Arrange
        var request = new DeleteCategoryRequest(id);
        var validator = new DeleteCategoryRequestValidator();
        var cancellationToken = TestContext.Current.CancellationToken;

        await using DimaDbContext context = fixture.CreateContext();

        var handler = new DeleteCategoryHandler(
            context,
            validator);

        // Act
        Result result = await handler.HandleAsync(
            request,
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

    [Theory]
    [InlineData(1)]
    public async Task HandleAsync_ShouldReturnNotFound_WhenCategoryDoesNotExists(long id)
    {
        // Arrange
        var request = new DeleteCategoryRequest(id);
        var validator = new DeleteCategoryRequestValidator();
        var cancellationToken = TestContext.Current.CancellationToken;

        await using DimaDbContext context = fixture.CreateContext();

        var handler = new DeleteCategoryHandler(
            context,
            validator);

        // Act
        Result result = await handler.HandleAsync(
            request,
            cancellationToken);

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
}
