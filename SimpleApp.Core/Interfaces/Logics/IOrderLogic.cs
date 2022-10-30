using System;
using System.Collections.Generic;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IOrderLogic : ILogic
    {
        Result<IEnumerable<Order>> GetAllActive();

        Result<Order> GetById(Guid id);

        Result<Order> Add(Order order, Guid userId);

        Result<Order> Update(Order order);

        Result Delete(Order order);
    }
}
