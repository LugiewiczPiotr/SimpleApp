using System;
using System.Collections.Generic;
using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Logics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<Order> _validator;
        public OrderLogic(IOrderRepository orderRepository, IValidator<Order> validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        public Result<IEnumerable<Order>> GetAllActive()
        {
            var orders = _orderRepository.GetAllActive();

            return Result.Ok(orders);
        }

        public Result<Order> GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return Result.Failure<Order>($"Order with ID {id} does not exist.");
            }

            return Result.Ok(order);
        }

        public Result<Order> Add(Order order, Guid Id)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.UserId = Id;
            order.PlacedOn = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            order.Status = OrderStatus.Placed;

            var validationResult = _validator.Validate(order);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Order>(validationResult.Errors);
            }

            _orderRepository.Add(order);
            _orderRepository.SaveChanges();

            return Result.Ok(order);
        }

        public Result<Order> Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var validationResult = _validator.Validate(order);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Order>(validationResult.Errors);
            }

            if (order.Status == OrderStatus.Finalized)
            {
                order.FinalizedOn = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                order.CancelledOn = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            }

            _orderRepository.SaveChanges();

            return Result.Ok(order);
        }

        public Result Delete(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _orderRepository.Delete(order);
            _orderRepository.SaveChanges();

            return Result.Ok(order);
        }
    }
}
