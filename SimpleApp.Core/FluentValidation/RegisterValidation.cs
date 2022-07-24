using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class RegisterValidation : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        public RegisterValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
      
            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty")
                .EmailAddress().WithMessage(" ‘Email’ is not a valid email address.");
            RuleFor(x => x.Email).Must(login => !_userRepository.IsEmailExists(login))
                .WithMessage("That email is taken");

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty")
                 .Length(8, 40).WithMessage
                 ("Password length should contain from {MinLength} up to {MaxLength} characters");
            

        }
    }
}
