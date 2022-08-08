using System;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;

namespace SimpleApp.Core.Interfaces.Repositories
{
   public interface IProductRepository : IRepository<Product>
   {
        public void DeleteByCategoryId(Guid id);
   }
}
