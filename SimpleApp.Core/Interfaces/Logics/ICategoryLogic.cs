using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface ICategoryLogic : ILogic
    {
        Task<Result<IEnumerable<Category>>> GetAllActiveAsync();

        Task<Result<Category>> GetByIdAsync(Guid id);

        Task<Result<Category>> AddAsync(Category category);

        Task<Result<Category>> UpdateAsync(Category category);

        Task<Result> DeleteAsync(Category category);
    }
}
