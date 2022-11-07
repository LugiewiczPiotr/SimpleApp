using System.Collections.Generic;

namespace SimpleApp.Core.Models.Entity
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
