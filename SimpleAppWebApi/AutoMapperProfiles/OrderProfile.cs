using AutoMapper;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();

            CreateMap<ManageOrderDto, Order>();
        }
    }
}
