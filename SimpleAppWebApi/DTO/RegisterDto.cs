using System;

namespace SimpleApp.WebApi.DTO
{
    public class RegisterDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
