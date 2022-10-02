using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool CheckIfUserExists(string email);
        public User GetUserByEmail(string email);
    }
}
