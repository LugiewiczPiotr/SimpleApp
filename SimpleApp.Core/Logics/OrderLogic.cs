using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleApp.Core.Logics
{
    public class OrderLogic : IOrderLogic
    {
        public enum status { Pending, Processing, InTransit, Delivered }
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
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
       
            order.UserId = Id;
            order.Date = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            order.Status = status.Pending.ToString();

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
            if(validationResult.IsValid == false)
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
