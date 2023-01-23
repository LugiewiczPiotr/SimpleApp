using System.Threading.Tasks;
using SimpleApp.Core.Models;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IUserLogic : ILogic
    {
        Task<Result<User>> CreateAccountAsync(User user);

        Result<string> Authenticate(UserLoginAndPassword userLoginAndPassword);
    }
}
