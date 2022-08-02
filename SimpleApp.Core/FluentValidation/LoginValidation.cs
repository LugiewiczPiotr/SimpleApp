using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using SimpleApp.Core.Interfaces.Logics;

namespace SimpleApp.Core.FluentValidation
{
    public class LoginValidation : AbstractValidator<UserLoginAndPassword>
    {

        public LoginValidation(IUserRepository userRepository, IAccountService accountService)
        {   

            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Email).Must(login => userRepository.IsEmailExists(login))
                .WithMessage("Email or password is invalid");

          
            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Password).Must((x,y) => accountService.VerifyPassword(x)).
                WithMessage("Email or password is invalid"); 
        }
    }
}
