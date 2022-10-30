using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class OrderValidatior : AbstractValidator<Order>
    {
        public OrderValidatior(IProductRepository productRepository)
        {
            RuleForEach(x => x.OrderItems).ChildRules(orders =>
            {
                orders.RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("This field cannot be empty")
                .Must(product => productRepository.CheckIfProductExist(product))
                .WithMessage("Product dont exist");

                orders.RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("This field cannot be empty")
                .GreaterThan(0).WithMessage("Value must be greater than 0");
            });
        }
    }
}
