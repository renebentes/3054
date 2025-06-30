using FluentValidation;

namespace Dima.Core.Categories.UpdateCategory;

/// <summary>
/// Validator for UpdateCategoryRequest.
/// </summary>
internal sealed class UpdateCategoryRequestValidator
    : AbstractValidator<UpdateCategoryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCategoryRequestValidator"/> class.
    /// </summary>
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(Title.MaxLength);

        RuleFor(x => x.Description)
            .MaximumLength(Description.MaxLength);
    }
}
