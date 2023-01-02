using System;

namespace SimpleApp.Core.Models.Entities
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
