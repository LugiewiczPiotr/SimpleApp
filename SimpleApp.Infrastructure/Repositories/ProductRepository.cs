using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;
using SimpleApp.Infrastructure.Repositories;
using System;
using System.Linq;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        public ProductRepository(AppDbContext db) : base(db)
        {
            
        }
        

        public override Product GetById(Guid id)
        {

            Context.Products.Include(c => c.Category).FirstOrDefault(e => e.Id == id && e.IsActive);
            return base.GetById(id);
        }
                
                



           
        
    }
}
