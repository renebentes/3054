using Dima.Core.Categories;
using Dima.Core.Primitives;

namespace Dima.UnitTests.Core.Categories;

public class DescriptionTests
{
    [Fact]
    public void CreateDescriptionInstance()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Description(random.String());

        // Assert
        action.Should()
            .NotThrow<DomainException>();
    }

    [Fact]
    public void CreateDescriptionShouldReturnDomainExceptionWhenValueLenghtIsGreaterAllowed()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Description(random.String(256));

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void CreateDescriptionShouldReturnEmptyWhenNullOrEmpty(string? text)
    {
        // Arrange, Act
        var description = new Description(text!);

        // Assert
        description.Text.Should().BeEmpty();
    }

    [Fact]
    public void ImplicitConversionFromStringShouldReturnDomainExceptionWhenValueLenghtIsGreaterAllowed()
    {
        // Arrange
        var random = new Randomizer();
        Description description;

        // Act
        Action action = () => description = random.String(256);

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Theory]
    [InlineData("test description")]
    [InlineData("")]
    [InlineData("test")]
    public void ToStringShouldReturnTextProperty(string text)
    {
        // Arrange, Act
        var description = new Description(text!);

        // Assert
        description.ToString().Should().Be(description.Text);
    }
}
