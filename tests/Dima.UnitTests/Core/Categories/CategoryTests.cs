using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class CategoryTests
{
    private readonly Title _title = new("Category Title");
    private Category? _testCategory;

    [Fact]
    public void Category_CreateInstance_WhenTitleIsValid()
    {
        _testCategory = CreateCategory();

        _testCategory.Title.Should().Be(_title);
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
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    private Category CreateCategory()
        => new(_title);
}
