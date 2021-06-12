using System.Collections.Generic;

namespace SimpleApp.Web.Models
{
    public class IndexViewModel
    {
        public IList<CategoryViewModel> CategoriesViewModels { get; set; }
        public IList<ProductViewModel> ProductsViewModels { get; set; }
    }
}
