using SimpleApp.Core.Models;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Interfaces.Services
{
    public interface IAccountService
    {
        string GenerateJwt(User user);
        bool ValidatePasswordStrength(string password);
        bool VerifyPassword(UserLoginAndPassword userLoginAndPassword);
    }
}
