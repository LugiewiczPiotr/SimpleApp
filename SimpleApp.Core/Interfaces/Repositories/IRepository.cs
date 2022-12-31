using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> AddAsync(T entity);

        void Delete(T entity);

        Task<IEnumerable<T>> GetAllActiveAsync();

        Task<T> GetByIdAsync(Guid id);

        Task SaveChangesAsync();
    }
}