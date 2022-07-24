using FluentValidation;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace SimpleApp.Core.FluentValidation
{
    public class LoginValidation : AbstractValidator<UserLoginAndPassword>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginValidation(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

            RuleFor(x => x.Email).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Email).Must(login => _userRepository.IsEmailExists(login))
                .WithMessage("Email or password is invalid");

            var user = _userRepository.GetAccesToDataUsers();
            RuleFor(x => x.Password).NotEmpty().WithMessage("This field cannot be empty");
            RuleFor(x => x.Password).Must(
                verifyPassword  => _passwordHasher.VerifyHashedPassword(
                   user, user.Password , verifyPassword) == PasswordVerificationResult.Failed)
                .WithMessage("Email or password is invalid");              
        }
    }
}
