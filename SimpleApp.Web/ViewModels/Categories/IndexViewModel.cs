using System;
using System.Collections.Generic;

namespace SimpleApp.Web.ViewModels.Categories
{
    public class IndexViewModel
    {
        public IList<IndexItemViewModel> Categories { get; set; }
    }

    public class IndexItemViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
