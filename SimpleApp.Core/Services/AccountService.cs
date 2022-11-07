using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entity;
using SimpleApp.Core.Models.Settings;

namespace SimpleApp.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtSettings _jwtSettings;

        public AccountService(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IOptionsSnapshot<JwtSettings> optionsSnapshot)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtSettings = optionsSnapshot.Value;
        }

        public string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.JwtExpireHours),
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
            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])");
            return regex.IsMatch(password);
        }

        public bool VerifyPassword(UserLoginAndPassword userLoginAndPassword)
        {
            var user = _userRepository.GetUserByEmail(userLoginAndPassword.Email);
            if (user == null)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                userLoginAndPassword.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return false;
            }

            return true;
        }
    }
}
