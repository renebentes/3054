using Dima.Api.Categories.CreateCategory;
using Dima.Api.Data;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
{
    private readonly CancellationToken _cancellationToken;
    private readonly CreateCategoryHandler _handler;

    public CreateCategoryHandlerTests()
    {
        _cancellationToken = TestContext.Current.CancellationToken;
        IApplicationDbContext context = Substitute.For<IApplicationDbContext>();
        IValidator<CreateCategoryRequest> validator = new CreateCategoryRequestValidator();

        _handler = new CreateCategoryHandler(
            context,
            validator);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            "Category title",
            "Category description");

        // Act
        var result = await _handler.HandleAsync(
            createCategoryRequest,
            _cancellationToken);

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

        // Act
        var result = await _handler.HandleAsync(
            createCategoryRequest,
            _cancellationToken);

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
}
