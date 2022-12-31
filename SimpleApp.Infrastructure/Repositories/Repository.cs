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
        protected Repository(AppDbContext dataContext)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected readonly AppDbContext _context;

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);
        }

        public virtual async Task<IEnumerable<T>> GetAllActiveAsync()
        {
            return await _context.Set<T>()
                .Where(e => e.IsActive)
                .ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>()
                .AddAsync(entity);

            return entity;
        }

        public virtual void Delete(T entity)
        {
            entity.IsActive = false;
        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
