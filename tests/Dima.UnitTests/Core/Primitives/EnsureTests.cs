namespace Dima.UnitTests.Core.Primitives;

public class EnsureTests
{
    private readonly Error _error = new("Error.Test", "Error description");

    [Theory]
    [InlineData("")]
    [InlineData("text")]
    [InlineData("tests")]
    public void MaxLength_ShouldReturnSameString_WhenLengthIsWithinLimit(string input)
    {
        string result = Ensure.MaxLength(input, 5, _error);

        result.ShouldBe(input);
    }

    [Fact]
    public void MaxLength_ThrowsArgumentNullException_WhenInputIsNull()
    {
        Action action = () => Ensure.MaxLength(null!, 10, _error);

        action.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void MaxLength_ThrowsArgumentOutOfRangeException_WhenMaxLengthIsNegative()
    {
        Action action = () => Ensure.MaxLength("test", -1, _error);

        action.ShouldThrow<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void MaxLength_ThrowsDomainException_WhenInputExceedsMaxLength()
    {
        Action action = () => Ensure.MaxLength("test", 3, _error);

        action.ShouldThrow<DomainException>(_error).Error.ShouldBe(_error);
    }

    [Theory]
    [InlineData("test")]
    [InlineData(" test ")]
    [InlineData("1")]
    public void Required_ReturnsInput_WhenInputIsNotWhiteSpace(string input)
    {
        var result = Ensure.Required(input, _error);

        result.ShouldBe(input);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData("\r")]
    [InlineData(null)]
    public void Required_ThrowsDomainException_WhenInputIsNullOrWhiteSpace(string? input)
    {
        Action action = () => Ensure.Required(input!, _error);

        action.ShouldThrow<DomainException>(_error).Error.ShouldBe(_error);
    }
}
