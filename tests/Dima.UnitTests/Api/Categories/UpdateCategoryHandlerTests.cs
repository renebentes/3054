using Dima.Api.Categories.UpdateCategory;
using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;

namespace Dima.UnitTests.Api.Categories;

public class UpdateCategoryHandlerTests
{
    private readonly CancellationToken _cancellationToken;
    private readonly UpdateCategoryHandler _handler;
    private readonly ICategoryRepository _repositoryMock = Substitute.For<ICategoryRepository>();

    public UpdateCategoryHandlerTests()
    {
        var validator = new UpdateCategoryRequestValidator();
        _cancellationToken = TestContext.Current.CancellationToken;
        _handler = new UpdateCategoryHandler(
            _repositoryMock,
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

        await _repositoryMock
            .DidNotReceive()
            .GetByIdAsync(Arg.Any<long>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "Test", "Desc");

        _repositoryMock.GetByIdAsync(request.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Category?>(null));

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

        _repositoryMock
        .DidNotReceive()
        .Update(Arg.Any<Category>());
    }

    [Fact]
    public async Task HandleAsync_ShouldUpdateCategory_WhenRequestIsValid()
    {
        // Arrange
        var request = new UpdateCategoryRequest(1, "New Title", "New Desc");
        var category = new Category(new Title("Old Title"));
        _repositoryMock.GetByIdAsync(request.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Category?>(category));
        _repositoryMock.UnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(1));

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

        _repositoryMock
        .Received(1)
        .Update(category);

        await _repositoryMock
        .UnitOfWork
        .Received(1)
        .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
