using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IOrderLogic : ILogic
    {
        Task<Result<IEnumerable<Order>>> GetAllActiveOrders(Guid userId);

        Task<Result<Order>> GetById(Guid id);

        Task<Result<Order>> Add(Order order, Guid userId);

        Task<Result<Order>> Update(Order order);

        Result Delete(Order order);
    }
}
