using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class LoginValidatior : AbstractValidator<UserLoginAndPassword>
    {
        public LoginValidatior(IUserRepository userRepository, IAccountService accountService)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty")
            .Must(login => userRepository.CheckIfUserExists(login))
            .WithMessage("Login is invalid");

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty")
            .Must((x, y) => accountService.VerifyPassword(x)).
             WithMessage("Password is invalid");
        }
    }
}
