using Dima.Api.Categories.CreateCategory;
using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation;

namespace Dima.UnitTests.Api.Categories;

public sealed class CreateCategoryHandlerTests
{
    private readonly CancellationToken _cancellationToken;
    private readonly CreateCategoryHandler _handler;
    private readonly ICategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandlerTests()
    {
        _cancellationToken = TestContext.Current.CancellationToken;
        _repository = Substitute.For<ICategoryRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        IValidator<CreateCategoryRequest> validator = new CreateCategoryRequestValidator();

        _handler = new CreateCategoryHandler(
            _repository,
            _unitOfWork,
            validator);
    }

    [Theory]
    [InlineData("test title", "")]
    [InlineData("test title", " ")]
    [InlineData("test title", null)]
    [InlineData("test title", "test description")]
    public async Task HandleAsync_ShouldReturnCreated_WhenRequestIsValid(
        string title,
        string? description)
    {
        // Arrange
        var createCategoryRequest = new CreateCategoryRequest(
            title,
            description!);

        // Act
        var result = await _handler.HandleAsync(
            createCategoryRequest,
            _cancellationToken);

        // Assert
        await _repository
            .Received(1)
            .AddAsync(Arg.Any<Category>(), _cancellationToken);

        await _unitOfWork
            .Received(1)
            .SaveChangesAsync(_cancellationToken);

        result
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeTrue();
        result
            .Status
            .Should()
            .Be(ResultStatus.Created);
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
            .Should()
            .NotBeNull();
        result
            .IsSuccess
            .Should()
            .BeFalse();
        result
            .Status
            .Should()
            .Be(ResultStatus.Invalid);
    }
}
