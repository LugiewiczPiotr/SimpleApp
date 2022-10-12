using System;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public bool CheckIfProductExist(Guid Id);
    }
}
