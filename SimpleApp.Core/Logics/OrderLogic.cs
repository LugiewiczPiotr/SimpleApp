using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using SimpleApp.Core.Enums;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;

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

        public async Task<Result<IEnumerable<Order>>> GetAllActiveOrdersAsync(Guid userId)
        {
            var orders = await _orderRepository.GetAllActiveOrdersAsync(userId);

            return Result.Ok(orders);
        }

        public async Task<Result<Order>> GetByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return Result.Failure<Order>($"Order with ID {id} does not exist.");
            }

            return Result.Ok(order);
        }

        public async Task<Result<Order>> AddAsync(Order order, Guid userId)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            order.UserId = userId;

            var validationResult = await _validator.ValidateAsync(order);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Order>(validationResult.Errors);
            }

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return Result.Ok(order);
        }

        public async Task<Result<Order>> UpdateAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

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

            var validationResult = await _validator.ValidateAsync(order);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Order>(validationResult.Errors);
            }

            await _orderRepository.SaveChangesAsync();

            return Result.Ok(order);
        }

        public Result Delete(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            _orderRepository.Delete(order);
            _orderRepository.SaveChangesAsync();

            return Result.Ok(order);
        }
    }
}
