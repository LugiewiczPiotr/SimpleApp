using FluentValidation;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class ProductValidatior : AbstractValidator<Product>
    {
        public ProductValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(3).WithMessage("Minimum length of {MinLength} char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of {MaxLength} char is allowed");
            RuleFor(x => x.Description).NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(5).WithMessage("Minimum length of {MinLength} char allowed")
                .MaximumLength(50).WithMessage("Maximum length of {MaxLength} char is allowed");
            RuleFor(x => x.Price).NotEmpty().WithMessage("This field cannot be empty")
                .ScalePrecision(2, 7).WithMessage(" Price must not be more than 7 digits in total,with allowance for 2 decimals.");
            RuleFor(x => x.Category).NotNull().WithMessage("Category cannot be null");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("value must be greater than 0");
        }
    }
}
