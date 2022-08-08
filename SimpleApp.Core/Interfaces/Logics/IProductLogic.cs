using System;
using System.Collections.Generic;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IProductLogic : ILogic
    {
        Result<IEnumerable<Product>> GetAllActive();

        Result<Product> GetById(Guid id);

        Result<Product> Add(Product product);

        Result<Product> Update(Product product);

        Result Delete(Product product);
    }
}
