using System;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Interfaces.Services;
using SimpleApp.Core.Models;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Logics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _registerValidator;
        private readonly IValidator<UserLoginAndPassword> _loginValidator;
        private readonly IAccountService _accountService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserLogic(
            IUserRepository userRepository,
            IValidator<User> registerValidator,
            IAccountService accountService,
            IValidator<UserLoginAndPassword> loginValidator,
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
            ArgumentNullException.ThrowIfNull(nameof(userLoginAndPassword));

            var validationResult = _loginValidator.Validate(userLoginAndPassword);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<string>(validationResult.Errors);
            }

            var user = _userRepository.GetUserByEmail(userLoginAndPassword.Email);
            var token = _accountService.GenerateJwt(user);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure<string>("Something went wrong while generating the token");
            }

            return Result.Ok(token);
        }

        public Result<User> CreateAccount(User user)
        {
            ArgumentNullException.ThrowIfNull(nameof(user));

            var validationResult = _registerValidator.Validate(user);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<User>(validationResult.Errors);
            }

            var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
            user.Password = hashedPassword;
            _userRepository.AddAsync(user);
            _userRepository.SaveChangesAsync();

            return Result.Ok(user);
        }
    }
}
