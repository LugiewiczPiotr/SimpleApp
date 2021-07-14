using System;
using System.Collections.Generic;

namespace SimpleApp.Web.ViewModels.Products
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
       
        public decimal Price { get; set; }

        public IEnumerable<SelectItemViewModel> AvailableCategories { get; set; }
        public Guid Category { get; set; }
    }
}
