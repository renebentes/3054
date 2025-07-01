using Dima.Api.Categories.CreateCategory;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
{
    private readonly CancellationToken _cancellationToken;
    private readonly CreateCategoryHandler _handler;
    private readonly ICategoryRepository _repositoryMock = Substitute.For<ICategoryRepository>();

    public CreateCategoryHandlerTests()
    {
        _cancellationToken = TestContext.Current.CancellationToken;
        IValidator<CreateCategoryRequest> validator = new CreateCategoryRequestValidator();

        _handler = new CreateCategoryHandler(
            _repositoryMock,
            validator);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            "Category title",
            "Category description");

        _repositoryMock
            .UnitOfWork
            .SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(1));

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

        await _repositoryMock
            .Received(1)
            .AddAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());

        await _repositoryMock
            .UnitOfWork
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
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

        await _repositoryMock
            .DidNotReceive()
            .AddAsync(Arg.Any<Category>(), Arg.Any<CancellationToken>());
    }
}
