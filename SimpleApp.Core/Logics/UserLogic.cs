﻿using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleApp.Core.Logics
{
    public class UserLogic : IUserLogic 
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;
        private readonly IAccountService _accountService;
        public UserLogic(IUserRepository userRepository, IValidator<User> validator,
            IAccountService accountService)
        {
            _userRepository = userRepository;
            _validator = validator;
            _accountService = accountService;
        }

        public Result<string> Authenticate(string login, string password)
        {
            var registeredUser = _userRepository.IsEmailExists(login);
            var passwordValidate = _userRepository.IsPasswordExists(password);

            if (registeredUser == false || passwordValidate == false )
            {
                return Result.Failure<string>($"Email or password is invalid");
            }
            if (registeredUser == false && passwordValidate == false)
            {
                return Result.Failure<string>($"Email or password is invalid");
            }

            var token = _accountService.GenerateJwt(login);
            return Result.Ok(token);
        }

        public Result<User> CreateAccount(User user)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var validationResult = _validator.Validate(user);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<User>(validationResult.Errors);
            }

            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return Result.Ok(user);
        }
    }
}
