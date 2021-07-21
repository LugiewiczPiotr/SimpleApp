using SimpleApp.Web.Models;
using System;
using System.Collections.Generic;

namespace SimpleApp.Web.ViewModels.Products
{
    public class IndexViewModel
    {
        public IList<IndexItemViewModel> Products { get; set; }
    }

    public class IndexItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }


}
