using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class RegisterValidation : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountService _accountService;
        public RegisterValidation(IUserRepository userRepository, IAccountService accountService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
      
            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty")
                .EmailAddress().WithMessage(" ‘Email’ is not a valid email address.");

            RuleFor(x => x.Email).Must(login => !_userRepository.IsEmailExists(login))
                .WithMessage("That email is taken");

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty")
                 .Length(8, 40).WithMessage
                 ("Password length should contain from {MinLength} up to {MaxLength} characters");

            RuleFor(x => x.Password).Must(password => _accountService
            .ValidatePasswordStrenght(password))
                .WithMessage("The password should contain one uppercase letter, one lower case letter and one number");
            

        }
    }
}
