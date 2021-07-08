using AutoMapper;
using SimpleApp.Core.Models;
using SimpleApp.Web.Models;

namespace SimpleApp.Web.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
