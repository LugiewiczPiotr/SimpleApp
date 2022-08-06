using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IUserLogic : ILogic
    {
        Result<User> CreateAccount(User user);
        Result<string> Authenticate(UserLoginAndPassword userLoginAndPassword);
    }
}
