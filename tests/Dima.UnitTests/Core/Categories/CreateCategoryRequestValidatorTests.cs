using Dima.Core.Categories;
using Dima.Core.Categories.CreateCategory;
using FluentValidation.TestHelper;

namespace Dima.UnitTests.Core.Categories;

public sealed class CreateCategoryRequestValidatorTests
{
    private readonly CreateCategoryRequestValidator _validator = new();

    [Fact]
    public void Validate_IsInvalid_WhenDescriptionExceedsMaximumLength()
    {
        var request = new CreateCategoryRequest(
            "Title",
            new string('d', Description.MaxLength + 1));

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_IsInvalid_WhenTitleExceedsMaximumLength()
    {
        var request = new CreateCategoryRequest(
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
        var request = new CreateCategoryRequest(
            title!,
            "Description");

        _validator
            .TestValidate(request)
            .ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void Validate_IsValid_WhenDescriptionIsEmpty()
    {
        var request = new CreateCategoryRequest(
            "Title",
            string.Empty);

        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_IsValid_WhenDescriptionIsNull()
    {
        var request = new CreateCategoryRequest(
            "Title",
            null);

        _validator
            .TestValidate(request)
            .ShouldNotHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validate_IsValid_WhenTitleAndDescriptionAreAtMaximumLength()
    {
        var request = new CreateCategoryRequest(
            new string('t', Title.MaxLength),
            new string('d', Description.MaxLength));

        _validator
            .TestValidate(request)
            .ShouldNotHaveAnyValidationErrors();
    }
}
