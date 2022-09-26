using AutoMapper;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class ManageOrderProfile : Profile
    {
        public ManageOrderProfile()
        {
            CreateMap<ManageOrderDto, Order>();
        }
    }
}