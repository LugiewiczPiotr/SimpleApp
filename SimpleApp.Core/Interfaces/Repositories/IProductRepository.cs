using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;
using System;

namespace SimpleApp.Core.Interfaces.Repositories
{
   public interface IProductRepository : IRepository<Product>
   {
        public void DeleteByCategoryId(Guid id);
   }
}
