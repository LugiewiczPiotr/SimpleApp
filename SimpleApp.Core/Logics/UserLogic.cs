using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using System;

namespace SimpleApp.Core.Logics
{
    public class UserLogic : IUserLogic 
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _registerValidator;
        private readonly IValidator<UserLoginAndPassword> _loginValidator;
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserLogic(IUserRepository userRepository, IValidator<User> registerValidator,
            IAccountService accountService,IValidator<UserLoginAndPassword> loginValidator,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _accountService = accountService;
            _passwordHasher = passwordHasher;
        }

        public Result<string> Authenticate(UserLoginAndPassword userLoginAndPassword)
        {
            if(userLoginAndPassword == null)
            {
                throw new ArgumentNullException(nameof(userLoginAndPassword));
            }
            
            var validationResult = _loginValidator.Validate(userLoginAndPassword);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<string>(validationResult.Errors);
            }

            var user = _userRepository.GetAccesToDataUsers(userLoginAndPassword.Email);
            var token = _accountService.GenerateJwt(user);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<string>(token);
            }

            return Result.Ok(token);
        }

        public Result<User> CreateAccount(User user)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var validationResult = _registerValidator.Validate(user);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<User>(validationResult.Errors);
            }

            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return Result.Ok(user);
        }
    }
}
