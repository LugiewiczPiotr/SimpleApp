using AutoMapper;
using SimpleApp.Core.Models;
using SimpleApp.Web.ViewModels.Categories;
using SimpleApp.Web.ViewModels.Products;

namespace SimpleApp.Web.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Product, ViewModels.Products.IndexItemViewModel>();
        }
    }
}
