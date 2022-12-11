using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface ICategoryLogic : ILogic
    {
        Task <Result<IEnumerable<Category>>> GetAllActive();

        Task <Result<Category>> GetById(Guid id);

        Task<Result<Category>> Add(Category category);

        Task<Result<Category>> Update(Category category);

        Result Delete(Category category);
    }
}
