using Dima.Core.Categories;
using Dima.Core.Primitives;

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
        result.Should().Be(title.ToString());
    }

    [Fact]
    public void Title_CreateInstance_WhenValid()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String());

        // Assert
        action.Should()
            .NotThrow<DomainException>();
    }

    [Fact]
    public void Title_ThrowsDomainException_WhenValueExceedsMaxLenght()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String(Title.MaxLength + 1));

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Title_ThrowsDomainException_WhenValueIsNullOrEmpty(string? text)
    {
        Action action = () => _ = new Title(text!);

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ToString_ReturnsValue(string text)
    {
        // Arrange, Act
        var title = new Title(text!);

        // Assert
        title.ToString().Should().Be(title.Value);
    }
}
