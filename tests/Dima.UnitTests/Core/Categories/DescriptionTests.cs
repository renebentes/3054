using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class DescriptionTests
{

    [Fact]
    public void Description_AcceptsText_WhenTextIsAtMaxLength()
    {
        var expected = new string('a', Description.MaxLength);

        var description = new Description(expected);

        description.Text.ShouldBe(expected);
    }

    [Fact]
    public void Description_AcceptsTrimmedText_WhenInputExceedsMaxLengthBeforeTrimming()
    {
        var expected = new string('a', Description.MaxLength);
        var text = $" {expected} ";

        var description = new Description(text);

        description.Text.ShouldBe(expected);
    }
    [Fact]
    public void Description_CreateInstance_WhenValid()
    {
        const string expected = "test description";

        var description = new Description(expected);

        description.Text.ShouldBe(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void Description_ReturnsEmpty_WhenTextIsWhiteSpace(string? text)
    {
        // Arrange, Act
        var description = new Description(text!);

        // Assert
        description
            .Text
            .ShouldBeEmpty();
    }

    [Fact]
    public void Description_ThrowsArgumentNullException_WhenTextIsNull()
    {
        Action action = () => _ = new Description(null!);

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void Description_ThrowsDomainException_WhenTextExceedsMaxLength()
    {
        var text = new string('a', Description.MaxLength + 1);

        Action action = () => _ = new Description(text);

        // Assert
        action.ShouldThrow<DomainException>(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Fact]
    public void Description_ThrowsDomainException_WhenTrimmedTextExceedsMaxLength()
    {
        var text = new string('a', Description.MaxLength + 1) + " ";

        Action action = () => _ = new Description(text);

        action.ShouldThrow<DomainException>(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("  test description  ", "test description")]
    [InlineData("\t test description \n", "test description")]
    public void Description_TrimsWhiteSpace_WhenValueHasLeadingOrTrailingSpaces(string input, string expected)
    {
        var description = new Description(input);

        description.Text.ShouldBe(expected);
    }

    [Fact]
    public void Description_WithExpression_ReturnsEmpty_WhenTextIsWhiteSpace()
    {
        var description = new Description("original");

        var updatedDescription = description with { Text = " \t\n " };

        updatedDescription.Text.ShouldBeEmpty();
        description.Text.ShouldBe("original");
    }

    [Fact]
    public void Description_WithExpression_ThrowsArgumentNullException_WhenTextIsNull()
    {
        var description = new Description("original");

        Action action = () => _ = description with { Text = null! };

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void Description_WithExpression_ThrowsDomainException_WhenTextExceedsMaxLength()
    {
        var description = new Description("original");
        var text = new string('a', Description.MaxLength + 1);

        Action action = () => _ = description with { Text = text };

        action.ShouldThrow<DomainException>(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Fact]
    public void Description_WithExpression_ValidatesAndTrimsText()
    {
        var description = new Description("original");

        var updatedDescription = description with { Text = "  updated  " };

        updatedDescription.Text.ShouldBe("updated");
        description.Text.ShouldBe("original");
    }

    [Fact]
    public void Empty_HasEmptyText()
    {
        Description.Empty.Text.ShouldBeEmpty();
    }

    [Fact]
    public void ExplicitConversion_ReturnsDescription_FromString()
    {
        const string expected = "test description";

        Description description = (Description)expected;

        description.Text.ShouldBe(expected);
    }

    [Theory]
    [InlineData("test description", "test description")]
    [InlineData("  test description  ", "test description")]
    [InlineData(" ", "")]
    public void ImplicitConversion_ReturnsString_FromDescription(string text, string expected)
    {
        Description description = new(text);

        string actual = description;

        actual.ShouldBe(expected);
    }

    [Theory]
    [InlineData("test description")]
    [InlineData("")]
    [InlineData("test")]
    public void ToString_ReturnsText(string text)
    {
        // Arrange, Act
        var description = new Description(text);

        // Assert
        description.ToString()
            .ShouldBe(text.Trim());
    }
}
