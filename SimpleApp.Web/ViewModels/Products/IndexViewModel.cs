using SimpleApp.Web.Models;
using System.Collections.Generic;

namespace SimpleApp.Web.ViewModels.Product
{
    public class IndexViewModel
    {
        public IList<ProductViewModel> ProductsViewModels { get; set; }
    }
}
