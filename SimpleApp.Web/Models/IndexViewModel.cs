using System.Collections.Generic;

namespace SimpleApp.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<CategoryViewModel> CategoriesViewModels { get; set; }
    }
}
