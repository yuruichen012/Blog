using FastEndpoints;
using FluentValidation;

namespace PostManagement.Web.Categories;

public class CreateCategoryRequestValidator : Validator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.ParentId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Name).NotEmpty().MinimumLength(1);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}
