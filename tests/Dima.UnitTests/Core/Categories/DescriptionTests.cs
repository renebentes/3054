using Dima.Core.Categories;

namespace Dima.UnitTests.Core.Categories;

public class DescriptionTests
{
    [Fact]
    public void Description_CreateInstance_WhenValid()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Description(random.String());

        // Assert
        action.Should()
            .NotThrow<DomainException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Description_ReturnsEmpty_WhenTextIsNullOrEmpty(string? text)
    {
        // Arrange, Act
        var description = new Description(text!);

        // Assert
        description.Text.Should().BeEmpty();
    }

    [Fact]
    public void Description_ThrowsDomainException_WhenTextExceedsMaxLenght()
    {
        // Arrange
        var random = new Randomizer();

        // Act
        Action action = () => _ = new Description(random.String(Description.MaxLength + 1));

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage(DomainErrors.Description.LongerThanAllowed.ToString());
    }

    [Fact]
    public void ImplicitConversion_ReturnsDescription_FromString()
    {
        // Arrange
        var random = new Faker();
        var expected = random.Lorem.Sentence();

        // Act
        Description description = expected;

        // Assert
        description.Text.Should().Be(expected);
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
            .Should()
            .Be(description.Text);
    }
}
