using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entity;
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

        public override IEnumerable<Product> GetAllActive()
        {
            return Context.Products
                .Include(c => c.Category)
                 .Where(p => p.IsActive)
                 .ToList();
        }

        public override Product GetById(Guid id)
        {
           return Context.Products
                .Include(c => c.Category)
                .FirstOrDefault(e => e.Id == id && e.IsActive);
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
