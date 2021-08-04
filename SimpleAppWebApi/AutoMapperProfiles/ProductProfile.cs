using AutoMapper;
using SimpleApp.Core.Models;
using SimpleAppWebApi.DTO;

namespace SimpleApp.Web.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());

            
        }
    }
}
