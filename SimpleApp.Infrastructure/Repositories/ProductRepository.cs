using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;
using SimpleApp.Infrastructure.Data;
using Z.EntityFramework.Plus;

namespace SimpleApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db)
            : base(db)
        {
        }

        public override async Task<IEnumerable<Product>> GetAllActive()
        {
            return await Context.Products
                .Include(c => c.Category)
                 .Where(p => p.IsActive)
                 .ToListAsync();
        }

        public override async Task<Product> GetById(Guid id)
        {
           return await Context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public void DeleteByCategoryId(Guid id)
        {
            Context.Products
                .Where(e => e.CategoryId == id)
                .Update(x => new Product() { IsActive = false });
        }

        public bool CheckIfProductExist(Guid Id)
        {
            return Context.Products
                 .Where(p => p.Id == Id).Any();
        }
    }
}
