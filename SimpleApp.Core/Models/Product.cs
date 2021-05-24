namespace SimpleApp.Core.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
