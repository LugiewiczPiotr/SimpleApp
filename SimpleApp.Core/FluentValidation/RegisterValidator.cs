using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class RegisterValidator : AbstractValidator<User>
    {
        public RegisterValidator(IUserRepository userRepository, IAccountService accountService)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This field cannot be empty")
                .EmailAddress().WithMessage(" ‘Email’ is not a valid email address.")
                .Must(login => !userRepository.CheckIfUserExists(login))
                .WithMessage("That email is taken");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("This field cannot be empty")
                 .Length(8, 40).WithMessage("Password length should contain from " +
                 "{MinLength} up to {MaxLength} characters")
                 .Must(password => accountService
                 .ValidatePasswordStrength(password))
                 .WithMessage("The password should contain one uppercase letter," +
                 " one lower case letter and one number");
        }
    }
}
