using Chronos.Models.Category.Requests;
using FluentValidation;

namespace Chronos.Models.Category.Validators
{
    public class CategoryPostValidator: AbstractValidator<CategoryPost>
    {
        public CategoryPostValidator() {
            RuleFor(a => a.CategoryText).NotEmpty();
        }
    }
}
