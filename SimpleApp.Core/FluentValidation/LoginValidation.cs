using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.FluentValidation
{
    public class LoginValidation : AbstractValidator<UserLogin>
    {
        private readonly IUserRepository _userRepository;

        public LoginValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Email).
                Custom((value, context) =>
                {
                    var emailValidate = _userRepository.IsEmailExists(value);
                    if (emailValidate == false)
                    {
                        context.AddFailure("Email or password is invalid");
                    }
                });

            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Password).
               Custom((value, context) =>
               {
                   var emailValidate = _userRepository.IsPasswordExists(value);
                   if (emailValidate == false)
                   {
                       context.AddFailure("Email or password is invalid");
                   }
               });
        }
    }
}
