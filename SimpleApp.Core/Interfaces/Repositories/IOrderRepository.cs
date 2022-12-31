using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllActiveOrdersAsync(Guid userId);
    }
}
