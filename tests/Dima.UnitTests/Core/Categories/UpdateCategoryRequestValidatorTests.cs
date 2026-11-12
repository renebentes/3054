using Dima.Core.Categories;
using Dima.Core.Categories.UpdateCategory;
using FluentValidation.TestHelper;

namespace Dima.UnitTests.Core.Categories;

public sealed class UpdateCategoryRequestValidatorTests
{
    private readonly UpdateCategoryRequestValidator _validator = new();

    [Fact]
    public void Validate_IsInvalid_WhenDescriptionExceedsMaximumLength()
    {
        var request = new UpdateCategoryRequest(
            1,
            "Title",
            new string('d', Description.MaxLength + 1));

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(-1L)]
    public void Validate_IsInvalid_WhenIdIsNotPositive(long id)
    {
        var request = new UpdateCategoryRequest(
            id,
            "Title",
            "Description");

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Validate_IsInvalid_WhenTitleExceedsMaximumLength()
    {
        var request = new UpdateCategoryRequest(
            1,
            new string('t', Title.MaxLength + 1),
            "Description");

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Validate_IsInvalid_WhenTitleIsEmptyOrWhiteSpace(string? title)
    {
        var request = new UpdateCategoryRequest(
            1,
            title!,
            "Description");

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_IsValid_WhenDescriptionIsEmpty()
    {
        var request = new UpdateCategoryRequest(1, "Title", string.Empty);

        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_IsValid_WhenDescriptionIsNull_CurrentBehavior()
    {
        var request = new UpdateCategoryRequest(1, "Title", null!);

        _validator
            .TestValidate(request)
            .ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_IsValid_WhenIdIsPositive_AndTitleAndDescriptionAreAtMaximumLength()
    {
        var request = new UpdateCategoryRequest(
            1,
            new string('t', Title.MaxLength),
            new string('d', Description.MaxLength));

        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }
}
