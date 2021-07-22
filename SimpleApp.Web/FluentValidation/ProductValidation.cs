using FluentValidation;
using SimpleApp.Web.ViewModels.Products;

namespace SimpleApp.Web.FluentValidation
{
    public class ProductValidation : AbstractValidator<ProductViewModel>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).WithMessage("Minimum length of 5 char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of 20 char is allowed");
            RuleFor(x => x.Description).NotEmpty().MinimumLength(5).WithMessage("Minimum length of 5 char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of 20 char is allowed");
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
