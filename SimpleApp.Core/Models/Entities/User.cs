namespace SimpleApp.Core.Models.Entities
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
