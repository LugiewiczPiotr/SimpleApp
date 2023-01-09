using AutoMapper;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItemDto, OrderItem>()
                .ReverseMap();
        }
    }
}
