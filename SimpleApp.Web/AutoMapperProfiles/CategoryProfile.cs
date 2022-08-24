using System;
using AutoMapper;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.Web.ViewModels;
using SimpleApp.Web.ViewModels.Categories;
using SimpleApp.Web.ViewModels.Products;

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
    }
}