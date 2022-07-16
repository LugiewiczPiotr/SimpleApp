using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Repositories;

namespace SimpleApp.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool emailValidate(string email);
        public bool passwordValidate(string password);
    }
}
