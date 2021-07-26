using FluentValidation;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class ProductValidatior : AbstractValidator<Product>
    {
        public ProductValidatior()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(3).WithMessage("Minimum length of 5 char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of 20 char is allowed");
            RuleFor(x => x.Description).NotEmpty().WithMessage("This field cannot be empty")
                .MinimumLength(5).WithMessage("Minimum length of 5 char allowed")
                .MaximumLength(20).WithMessage("Maximum legth of 50 char is allowed");
            RuleFor(x => x.Price).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Category).NotNull();
            
            


        }
    }
}
