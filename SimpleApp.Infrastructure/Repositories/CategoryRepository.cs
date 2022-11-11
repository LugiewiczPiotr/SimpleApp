using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;
using SimpleApp.Infrastructure.Data;

namespace SimpleApp.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db)
            : base(db)
        {
        }
    }
}
