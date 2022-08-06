using Microsoft.IdentityModel.Tokens;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using SimpleApp.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace SimpleApp.Core.Logics
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public string GenerateJwt(User user)
        {
            var key = new ConfigurationBuilder().AddJsonFile("appsettings.json").
                Build().GetSection("Authentication")["JwtKey"];
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidatePasswordStrength(string password)
        {
            var regex = new Regex(("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])"));
            return regex.IsMatch(password);
        }

        public bool VerifyPassword(UserLoginAndPassword userLoginAndPassword)
        {
            var user = _userRepository.GetUserByEmail(userLoginAndPassword.Email);
            if (user == null)
            {
                return false;
            }
            
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password,
                userLoginAndPassword.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return false;
            }
            return true;
        }
    }
}
