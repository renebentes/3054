using Dima.Core.Categories;
using Dima.Core.Primitives;

namespace Dima.UnitTests.Core.Categories;

public class TitleTests
{
    [Fact]
    public void CreateTitleInstance()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String());

        // Assert
        action.Should()
            .NotThrow<DomainException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateTitleShouldReturnDomainExceptionWhenNullOrEmpty(string? text)
    {
        Action action = () => _ = new Title(text);

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.NullOrEmpty.ToString());
    }

    [Fact]
    public void CreateTitleShouldReturnDomainExceptionWhenValueLenghtIsGreaterAllowed()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Title(random.String(256));

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Title.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ImplicitOperatorShouldReturnToString(string text)
    {
        // Arrange
        var title = new Title(text!);

        // Act
        string result = title;

        // Assert
        result.Should().Be(title.ToString());
    }

    [Theory]
    [InlineData("test title")]
    [InlineData("test")]
    public void ToStringShouldReturnValueProperty(string text)
    {
        // Arrange, Act
        var title = new Title(text!);

        // Assert
        title.ToString().Should().Be(title.Value);
    }
}
