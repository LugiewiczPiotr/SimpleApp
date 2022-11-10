using FluentValidation;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(3).WithMessage("Minimum length of {MinLength} char allowed")
                .MaximumLength(20).WithMessage("Maximum length of {MaxLength} char is allowed");
        }
    }
}
