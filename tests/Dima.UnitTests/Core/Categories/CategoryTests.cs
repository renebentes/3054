using Dima.Core.Categories;
using Dima.Core.Primitives;

namespace Dima.UnitTests.Core.Categories;

public class CategoryTests
{
    private readonly Title _title = new("Category Title");
    private Category? _testCategory;

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateTitleShouldReturnDomainExceptionWhenNullOrEmpty(string? title)
    {
        Action action = () => _ = new Category(new Title(title!));

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Fact]
    public void InitializesCategoryShouldHaveTitle()
    {
        _testCategory = CreateCategory();

        _testCategory.Title.Should().Be(_title);
    }

    private Category CreateCategory()
        => new(_title);
}
