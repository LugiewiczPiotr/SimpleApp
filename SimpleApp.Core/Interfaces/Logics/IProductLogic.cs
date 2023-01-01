using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IProductLogic : ILogic
    {
        Task<Result<IEnumerable<Product>>> GetAllActiveAsync();

        Task<Result<Product>> GetByIdAsync(Guid id);

        Task<Result<Product>> AddAsync(Product product);

        Task<Result<Product>> UpdateAsync(Product product);

        Result Delete(Product product);
    }
}
