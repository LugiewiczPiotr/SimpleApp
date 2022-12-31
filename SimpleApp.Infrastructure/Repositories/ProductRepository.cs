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

        public override async Task<IEnumerable<Product>> GetAllActiveAsync()
        {
            return await _context.Products
                .Include(c => c.Category)
                 .Where(p => p.IsActive)
                 .ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(Guid id)
        {
           return await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public void DeleteByCategoryId(Guid id)
        {
            _context.Products
                .Where(e => e.CategoryId == id)
                .Update(x => new Product() { IsActive = false });
        }

        public async Task<bool> CheckIfProductExistAsync(Guid Id)
        {
            return await Context.Products
                 .Where(p => p.Id == Id).AnyAsync();
        }
    }
}
