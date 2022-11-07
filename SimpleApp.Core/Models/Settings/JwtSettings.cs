namespace SimpleApp.Core.Models.Settings
{
    public class JwtSettings
    {
        public const string SectionName = "JwtSettings";
        public string SecretKey { get; set; }
        public int JwtExpireHours { get; set; }
    }
}
