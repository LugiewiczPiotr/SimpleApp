using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;
using SimpleApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext db) : base(db)
        {
        }
    }
}
