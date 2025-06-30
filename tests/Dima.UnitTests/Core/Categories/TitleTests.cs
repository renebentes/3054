using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class TitleTests
{
    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ImplicitConversion_ReturnsToString(string text)
    {
        // Arrange
        var title = new Title(text!);

        // Act
        string result = title;

        // Assert
        result.ShouldBe(title.ToString());
    }

    [Fact]
    public void Title_CreateInstance_WhenValid()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String());

        // Assert
        action.ShouldNotThrow();
    }

    [Fact]
    public void Title_ThrowsDomainException_WhenValueExceedsMaxLenght()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String(Title.MaxLength + 1));

        // Assert
        action.ShouldThrow<DomainException>(DomainErrors.Title.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Title_ThrowsDomainException_WhenValueIsNullOrEmpty(string? text)
    {
        Action action = () => _ = new Title(text!);

        // Assert
        action.ShouldThrow<DomainException>(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ToString_ReturnsValue(string text)
    {
        // Arrange, Act
        var title = new Title(text!);

        // Assert
        title.ToString()
            .ShouldBe(title.Value);
    }
}
