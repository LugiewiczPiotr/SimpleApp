using System;
using System.Collections.Generic;
using FluentValidation;
using SimpleApp.Core.Enums;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entity;

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

        public Result<IEnumerable<Order>> GetAllActiveOrders(Guid userId)
        {
            var orders = _orderRepository.GetAllActiveOrders(userId);

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

        public Result<Order> Add(Order order, Guid userId)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            order.UserId = userId;

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

            switch (order.Status)
            {
                case OrderStatus.Finalized:
                    order.FinalizedAt = DateTime.UtcNow;
                    order.CancelledAt = null;
                    break;

                case OrderStatus.Cancelled:
                    order.CancelledAt = DateTime.UtcNow;
                    order.FinalizedAt = null;
                    break;

                case OrderStatus.Placed:
                    order.PlacedAt = DateTime.UtcNow;
                    order.CancelledAt = null;
                    order.FinalizedAt = null;
                    break;
            }

            var validationResult = _validator.Validate(order);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Order>(validationResult.Errors);
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
