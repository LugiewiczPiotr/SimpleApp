using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;
using SimpleApp.Infrastructure.Data;

namespace SimpleApp.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dataContext)
            : base(dataContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllActiveOrdersAsync(Guid userId)
        {
           return await Context.Orders
                .Include(x => x.OrderItems)
                .Where(u => u.UserId == userId && u.IsActive)
                .ToListAsync();
        }

        public override async Task<Order> GetByIdAsync(Guid id)
        {
            return await Context.Orders
                .Include(i => i.User)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }
    }
}
