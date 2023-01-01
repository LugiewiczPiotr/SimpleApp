using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IOrderLogic : ILogic
    {
        Task<Result<IEnumerable<Order>>> GetAllActiveOrdersAsync(Guid userId);

        Task<Result<Order>> GetByIdAsync(Guid id);

        Task<Result<Order>> AddAsync(Order order, Guid userId);

        Task<Result<Order>> UpdateAsync(Order order);

        Result Delete(Order order);
    }
}
