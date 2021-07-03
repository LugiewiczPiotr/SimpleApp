using SimpleApp.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace SimpleApp.Web.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryViewModel Category { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<SelectItemViewModel> AvailableCategories { get; set; }
        public Guid SelectedCategory { get; set; }
    }
}
