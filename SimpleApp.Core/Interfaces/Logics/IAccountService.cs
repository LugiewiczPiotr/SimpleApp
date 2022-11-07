using SimpleApp.Core.Models.Entity;

namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IAccountService
    {
        string GenerateJwt(User user);
        bool ValidatePasswordStrength(string password);
        bool VerifyPassword(UserLoginAndPassword userLoginAndPassword);
    }
}
