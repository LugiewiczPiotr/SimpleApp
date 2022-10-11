using System;
using AutoMapper;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.Web.ViewModels;
using SimpleApp.Web.ViewModels.Categories;

namespace SimpleApp.Web.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<Category, ViewModels.Categories.IndexItemViewModel>();
            CreateMap<Category, SelectItemViewModel>().ConvertUsing<ToSelectItemViewModelConverter>();

            CreateMap<Guid, Category>().ConvertUsing<GuidToCategoryConverter>();
            CreateMap<Category, Guid>().ConvertUsing<CategoryToGuidConverter>();
        }

        public class ToSelectItemViewModelConverter : ITypeConverter<Category, SelectItemViewModel>
        {
            public SelectItemViewModel Convert(Category source, SelectItemViewModel destination, ResolutionContext context)
            {
                return new SelectItemViewModel()
                {
                    Value = source.Id.ToString(),
                    Display = source.Name
                };
            }
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