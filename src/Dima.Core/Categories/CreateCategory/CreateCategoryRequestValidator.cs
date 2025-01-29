using FluentValidation;

namespace Dima.Core.Categories.CreateCategory;

/// <summary>
/// Represents the <see cref="CreateCategoryRequest"/> validator.
/// </summary>
internal sealed class CreateCategoryRequestValidator
    : AbstractValidator<CreateCategoryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCategoryRequestValidator"/> class.
    /// </summary>
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(ValidationErrors.CreateCategory.TitleIsRequired)
            .MaximumLength(Title.MaxLength);

        RuleFor(x => x.Description)
            .MaximumLength(Description.MaxLength);
    }
}
