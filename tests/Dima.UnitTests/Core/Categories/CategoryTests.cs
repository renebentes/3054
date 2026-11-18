using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class CategoryTests
{
    [Fact]
    public void Category_InitializesTitle_AndEmptyDescription()
    {
        var title = new Title("Category Title");

        var category = new Category(title);

        category.Title.ShouldBe(title);
        category.Description.ShouldBe(Description.Empty);
    }

    [Fact]
    public void Category_ThrowsArgumentNullException_WhenTitleIsNull()
    {
        Action action = () => _ = new Category(null!);

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ChangeDescription_AcceptsEmptyDescription()
    {
        var category = CreateCategory();

        category.ChangeDescription(Description.Empty);

        category.Description.Text.ShouldBeEmpty();
    }

    [Fact]
    public void ChangeDescription_ThrowsArgumentNullException_WhenDescriptionIsNull()
    {
        var category = CreateCategory();
        Action action = () => category.ChangeDescription(null!);

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ChangeDescription_UpdatesDescription_WhenDescriptionIsValid()
    {
        var category = CreateCategory();
        var description = new Description("Category description");

        category.ChangeDescription(description);

        category.Description.ShouldBe(description);
    }

    [Fact]
    public void ChangeTitle_ThrowsArgumentNullException_WhenTitleIsNull()
    {
        var category = CreateCategory();
        Action action = () => category.ChangeTitle(null!);

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void ChangeTitle_UpdatesTitle_WhenTitleIsValid()
    {
        var category = CreateCategory();
        var newTitle = new Title("New Category Title");

        category.ChangeTitle(newTitle);

        category.Title.ShouldBe(newTitle);
    }

    private static Category CreateCategory()
        => new(new Title("Category Title"));
}
