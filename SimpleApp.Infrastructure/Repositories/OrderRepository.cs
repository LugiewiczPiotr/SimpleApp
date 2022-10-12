using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;

namespace SimpleApp.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dataContext) : base(dataContext)
        {
        }

        public override IEnumerable<Order> GetAllActive()
        {
           return Context.Orders.Include(x => x.OrderItems)
                .Include(x => x.User)
                .Where(e => e.IsActive)
                .ToList();
        }

        public bool CheckIfProductExist(Guid Id)
        {
           return Context.Products.Where(p => p.Id == Id).Any();
        }

        public override Order GetById(Guid id)
        {
            return Context.Orders.Include(i => i.User)
                .Include(x => x.OrderItems)
                .FirstOrDefault(x => x.Id == id && x.IsActive);
               
        }
    }
}
