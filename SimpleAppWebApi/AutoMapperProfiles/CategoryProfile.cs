using System;
using AutoMapper;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
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