using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;
using System;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        public bool CheckIfProductExist(Guid Id);
    }
}
