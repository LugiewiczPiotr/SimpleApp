using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class OrderValidation : AbstractValidator<Order>
    {
        public OrderValidation(IOrderRepository orderRepository)
        {
            RuleForEach(x => x.OrderItems).ChildRules(orders =>
            {
                orders.RuleFor(x => x.ProductId).NotEmpty()
                .WithMessage("This field cannot be empty")
                .Must(product => orderRepository.CheckIfProductExist(product))
                .WithMessage("Product dont exist");

                orders.RuleFor(x => x.Quantity).NotEmpty()
                .WithMessage("This field cannot be empty")
                .GreaterThan(0).WithMessage("Value must be greater than 0");
            });
        }
    }
}
