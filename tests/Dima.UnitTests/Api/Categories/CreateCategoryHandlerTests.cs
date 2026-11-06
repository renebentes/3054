using Dima.Api.Categories.CreateCategory;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using Dima.UnitTests.Api.Configuring;
using FluentValidation;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
    : IClassFixture<DatabaseFixture>
{
    private readonly CancellationToken _cancellationToken;
    private readonly ICreateCategoryHandler _handler;
    private readonly ICategoryRepository _repositoryMock = Substitute.For<ICategoryRepository>();

    public CreateCategoryHandlerTests(DatabaseFixture fixture)
    {
        _cancellationToken = TestContext.Current.CancellationToken;
        IValidator<CreateCategoryRequest> validator = new CreateCategoryRequestValidator();

        _handler = new CreateCategoryHandler(
            fixture.CreateContext(),
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
