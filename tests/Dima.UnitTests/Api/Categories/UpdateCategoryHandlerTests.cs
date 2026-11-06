using Dima.Api.Categories.UpdateCategory;
using Dima.Api.Data;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;

namespace Dima.UnitTests.Api.Categories;

public class UpdateCategoryHandlerTests
    : IClassFixture<DatabaseFixture>
{
    private readonly CancellationToken _cancellationToken;
    private readonly UpdateCategoryHandler _handler;

    public UpdateCategoryHandlerTests(DatabaseFixture fixture)
    {
        var validator = new UpdateCategoryRequestValidator();
        _cancellationToken = TestContext.Current.CancellationToken;
        using DimaDbContext context = fixture.CreateContext();
        _handler = new UpdateCategoryHandler(
            context,
            validator
        );
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnInvalid_WhenValidationFails()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "", "Desc");

        // Act
        var result = await _handler.HandleAsync(request, _cancellationToken);

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

        // Act
        var result = await _handler.HandleAsync(request, _cancellationToken);

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
        var request = new UpdateCategoryRequest(1, "New Title", "New Desc");
        var category = new Category(new Title("Old Title"));

        // Act
        var result = await _handler.HandleAsync(request, _cancellationToken);

        // Assert
        result
            .ShouldNotBeNull();

        result
            .IsSuccess
            .ShouldBeTrue();

        result
            .Status
            .ShouldBe(ResultStatus.NoContent);
    }
}
