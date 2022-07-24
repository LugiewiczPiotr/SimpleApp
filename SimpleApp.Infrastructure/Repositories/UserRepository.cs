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

        public User GetAccesToDataUsers()
        { 
            return Context.Users.FirstOrDefault(); 
        }
    }
}
