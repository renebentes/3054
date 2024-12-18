using Bogus;
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
}
