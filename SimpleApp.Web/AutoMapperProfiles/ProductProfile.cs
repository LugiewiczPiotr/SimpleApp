using AutoMapper;
using SimpleApp.Core.Models.Entities;
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
