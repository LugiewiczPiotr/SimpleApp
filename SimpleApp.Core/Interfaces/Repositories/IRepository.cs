using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;

namespace SimpleApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T: BaseModel
    {
        T Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAllActive();
        T GetById(Guid id);
        void SaveChanges();
    }
}