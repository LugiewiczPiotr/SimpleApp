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

            CreateMap<Guid, Category>().ConvertUsing<GuidToCategoryConverter>();
            CreateMap<Category, Guid>().ConvertUsing<CategoryToGuidConverter>();
        }

        public class GuidToCategoryConverter : ITypeConverter<Guid, Category>
        {
            private readonly ICategoryLogic _categoryLogic;

            public GuidToCategoryConverter(ICategoryLogic categoryLogic)
            {
                _categoryLogic = categoryLogic;
            }

            public Category Convert(Guid source, Category destination, ResolutionContext context)
            {
                var categoryResult = _categoryLogic.GetById(source);

                if (categoryResult.Success)
                {
                    return categoryResult.Value;
                }

                return null;
            }
        }

        public class CategoryToGuidConverter : ITypeConverter<Category, Guid>
        {
            public Guid Convert(Category source, Guid destination, ResolutionContext context)
            {
                return new Guid(source.Id.ToString());
            }
        }
    }
}