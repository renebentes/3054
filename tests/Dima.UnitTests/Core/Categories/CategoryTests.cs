using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class CategoryTests
{
    private readonly Title _title = new("Category Title");
    private Category _testCategory = null!;

    [Fact]
    public void ChangeTitle_UpdatesTitle_WhenTitleIsValid()
    {
        // Arrange
        _testCategory = CreateCategory();
        var newTitle = new Title("New Category Title");

        // Act
        _testCategory.ChangeTitle(newTitle);

        // Assert
        _testCategory
            .Title
            .Should()
            .Be(newTitle);
    }

    [Fact]
    public void ChangeDescription_UpdatesDescription_WhenDescriptionIsValid()
    {
        // Arrange
        _testCategory = CreateCategory();
        var newDescription = new Description("New Category Title");

        // Act
        _testCategory.ChangeDescription(newDescription);

        // Assert
        _testCategory
            .Description
            .Should()
            .Be(newDescription);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ChangeTitle_ThrowsDomainException_WhenTitleIsInvalid(string? title)
    {
        // Arrange
        _testCategory = CreateCategory();

        // Act
        Action action = () =>
        {
            var invalidTitle = new Title(title!);
            _testCategory.ChangeTitle(invalidTitle);
        };

        // Assert
        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Category_ThrowsDomainException_WhenTitleIsInvalid(string? title)
    {
        // Act
        Action action = () => _ = new Category(new Title(title!));

        // Assert
        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    private Category CreateCategory()
        => new(_title);
}
