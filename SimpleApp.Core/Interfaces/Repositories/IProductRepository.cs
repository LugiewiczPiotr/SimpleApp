using System;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
   {
        public void DeleteByCategoryId(Guid id);
        Task<bool> CheckIfProductExist(Guid id);
    }
}
