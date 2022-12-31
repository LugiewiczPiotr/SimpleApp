using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;
using SimpleApp.Infrastructure.Data;

namespace SimpleApp.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : BaseModel, new()
    {
        protected readonly AppDbContext Context;

        protected Repository(AppDbContext dataContext)
        {
            Context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public virtual async Task<IEnumerable<T>> GetAllActiveAsync()
        {
            return await Context.Set<T>()
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>()
                .AddAsync(entity);

            return entity;
        }

        public virtual void Delete(T entity)
        {
            entity.IsActive = false;
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
