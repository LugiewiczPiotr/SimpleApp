using FluentValidation;
using SimpleApp.Web.ViewModels.Categories;

namespace SimpleApp.Web.FluentValidation
{
    public class CategoryValidation : AbstractValidator<CategoryViewModel>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).WithMessage("Minimum length of 3 char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of 20 char is allowed");
        }
    }
}
