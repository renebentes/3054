using FluentValidation;

namespace Dima.Core.Categories.DeleteCategory;

/// <summary>
/// Validator for <see cref="DeleteCategoryRequest"/>.
/// </summary>
public class DeleteCategoryRequestValidator
    : AbstractValidator<DeleteCategoryRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCategoryRequestValidator"/> class.
    /// </summary>
    public DeleteCategoryRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
