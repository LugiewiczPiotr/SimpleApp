using System;
using System.Collections.Generic;
using SimpleApp.Core.Models.Entity;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllActiveOrders(Guid userId);
    }
}
