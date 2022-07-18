using SimpleApp.Core.Models;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Infrastructure.Data;
using System.Linq;

namespace SimpleApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dataContext) : base(dataContext)
        {

        }

        public bool IsEmailExists(string email)
        {
            return Context.Users.Any(u => u.Email == email);
        }

        public bool IsPasswordExists(string password)
        {
            return Context.Users.Any(u => u.Password == password);
        }
    }
}
