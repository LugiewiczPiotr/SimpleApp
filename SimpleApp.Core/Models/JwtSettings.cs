namespace SimpleApp.Core.Models
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string SecretKey { get; set; }
    }
}
