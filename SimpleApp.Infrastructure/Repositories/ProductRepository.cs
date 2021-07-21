using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;
using System;
using System.Linq;

namespace SimpleApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(AppDbContext db) : base(db)
        {

        }

        public override Product GetById(Guid id)
        {
            Context.Products.Include(c => c.Category).
                FirstOrDefault(e => e.Id == id && e.IsActive);
            return base.GetById(id);

        }

        public void DeleteByCategoryId(Guid id)
        {
            var productCategory = Context.Products.Where(e => e.CategoryId == id).ToList();
            foreach (var elements in productCategory)
            {
                elements.IsActive = false;
            }
            
        }

        
    }
}
