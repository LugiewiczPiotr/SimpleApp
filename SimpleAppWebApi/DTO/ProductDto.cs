using System;

namespace SimpleApp.WebApi.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid Category { get; set; }
    }
}
