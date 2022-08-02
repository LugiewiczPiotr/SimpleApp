using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class RegisterValidation : AbstractValidator<User>
    {
        public RegisterValidation(IUserRepository userRepository, IAccountService accountService)
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty")
                .EmailAddress().WithMessage(" ‘Email’ is not a valid email address.");

            RuleFor(x => x.Email).Must(login => !userRepository.IsEmailExists(login))
                .WithMessage("That email is taken");

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty")
                 .Length(8, 40).WithMessage
                 ("Password length should contain from {MinLength} up to {MaxLength} characters");

            RuleFor(x => x.Password).Must(password => accountService
            .ValidatePasswordStrength(password))
                .WithMessage("The password should contain one uppercase letter, one lower case letter and one number");
            

        }
    }
}
