using System.Collections.Generic;

namespace SimpleApp.Core.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products{ get; set; }
    }
}
