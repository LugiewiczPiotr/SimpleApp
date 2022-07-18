using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool IsEmailExists(string email);
        public bool IsPasswordExists(string password);
    }
}
