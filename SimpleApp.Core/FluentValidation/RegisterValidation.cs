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

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty")
                 .MinimumLength(8).WithMessage("Minimum length of {MinLength} char allowed")
                 .MaximumLength(40).WithMessage("Maximum legth of {MaxLength} char is allowed");
            RuleFor(x => x.Email).
                Custom((value, context) =>
                {
                    var emailInUse = _userRepository.IsEmailExists(value);
                    if (emailInUse)
                    {
                        context.AddFailure("That email is taken");
                    }
                });

        }
    }
}
