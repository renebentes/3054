using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class TitleTests
{

    [Fact]
    public void ExplicitConversion_ReturnsTitle_FromString()
    {
        const string expected = "test title";

        Title title = (Title)expected;

        title.Value.ShouldBe(expected);
    }

    [Theory]
    [InlineData("test title", "test title")]
    [InlineData("test", "test")]
    public void ImplicitConversion_ReturnsStringValue(string text, string expected)
    {
        // Arrange
        var title = new Title(text);

        // Act
        string result = title;

        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void Title_CreateInstance_WhenValid()
    {
        const string expected = "test title";

        var title = new Title(expected);

        title.Value.ShouldBe(expected);
    }

    [Fact]
    public void Title_CreateInstance_WhenValueIsAtMaxLength()
    {
        var expected = new string('a', Title.MaxLength);

        var title = new Title(expected);

        title.Value.ShouldBe(expected);
    }

    [Fact]
    public void Title_ThrowsDomainException_WhenValueExceedsMaxLength()
    {
        var value = new string('a', Title.MaxLength + 1);

        Action action = () => _ = new Title(value);

        action.ShouldThrow<DomainException>(DomainErrors.Title.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData(null)]
    public void Title_ThrowsDomainException_WhenValueIsNullOrWhiteSpace(string? text)
    {
        Action action = () => _ = new Title(text!);

        action.ShouldThrow<DomainException>(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ToString_ReturnsValue(string text)
    {
        var title = new Title(text);

        title.ToString().ShouldBe(text);
    }

    [Fact]
    public void WithExpression_ThrowsDomainException_WhenValueExceedsMaxLength()
    {
        var title = new Title("original");
        var value = new string('a', Title.MaxLength + 1);

        Action action = () => _ = title with { Value = value };

        action.ShouldThrow<DomainException>(DomainErrors.Title.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void WithExpression_ThrowsDomainException_WhenValueIsNullOrWhiteSpace(string? value)
    {
        var title = new Title("original");

        Action action = () => _ = title with { Value = value! };

        action.ShouldThrow<DomainException>(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Fact]
    public void WithExpression_UpdatesValue_WithoutChangingOriginal()
    {
        var title = new Title("original");

        var updatedTitle = title with { Value = "updated" };

        updatedTitle.Value.ShouldBe("updated");
        title.Value.ShouldBe("original");
    }
}
