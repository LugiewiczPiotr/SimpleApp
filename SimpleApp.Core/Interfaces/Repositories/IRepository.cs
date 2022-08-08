using System;
using System.Collections.Generic;
using SimpleApp.Core.Models;

namespace SimpleApp.Infrastructure.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        T Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAllActive();
        T GetById(Guid id);
        void SaveChanges();
    }
}