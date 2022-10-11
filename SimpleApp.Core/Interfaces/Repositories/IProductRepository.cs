using System;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
   {
        public void DeleteByCategoryId(Guid id);
   }
}
