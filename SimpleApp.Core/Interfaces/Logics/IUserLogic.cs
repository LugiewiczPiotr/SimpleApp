using SimpleApp.Core.Models.Entity;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IUserLogic : ILogic
    {
        Result<User> CreateAccount(User user);
        Result<string> Authenticate(UserLoginAndPassword userLoginAndPassword);
    }
}
