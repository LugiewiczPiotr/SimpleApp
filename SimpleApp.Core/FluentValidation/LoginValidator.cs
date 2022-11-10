using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Interfaces.Services;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class LoginValidator : AbstractValidator<UserLoginAndPassword>
    {
        public LoginValidator(IUserRepository userRepository, IAccountService accountService)
        {
            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("This field cannot be empty")
            .Must(login => userRepository.CheckIfUserExists(login))
            .WithMessage("Email or password is invalid")
            .OverridePropertyName("Login Data");

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("This field cannot be empty")
            .Must((x, y) => accountService.VerifyPassword(x))
            .WithMessage("Email or password is invalid")
            .OverridePropertyName("Login Data");
        }
    }
}
