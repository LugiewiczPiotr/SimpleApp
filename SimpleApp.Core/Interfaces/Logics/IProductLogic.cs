using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IProductLogic : ILogic
    {
        Task<Result<IEnumerable<Product>>> GetAllActive();
        Task<Result<Product>> GetById(Guid id);
        Task<Result<Product>> Add(Product product); 
        Task<Result<Product>> Update(Product product);
        Result Delete(Product product);
    }
}
