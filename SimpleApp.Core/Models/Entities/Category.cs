using System.Collections.Generic;

namespace SimpleApp.Core.Models.Entities
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
