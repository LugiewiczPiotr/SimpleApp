using SimpleApp.Core.Models;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IAccountService : ILogic
    {
        string GenerateJwt(User user);
        bool ValidatePasswordStrength(string password);
        bool VerifyPassword(UserLoginAndPassword userLoginAndPassword);
    }
}
