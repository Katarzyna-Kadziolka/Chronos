using Chronos.Models.Category.Requests;
using FluentValidation;

namespace Chronos.Models.Category.Validators
{
    public class CategoryPatchValidator: AbstractValidator<CategoryPatch>
    {
        public CategoryPatchValidator() {
            RuleFor(a => a.Name).NotEmpty();
        }
    }
}
