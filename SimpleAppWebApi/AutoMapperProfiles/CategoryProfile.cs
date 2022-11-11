using AutoMapper;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}