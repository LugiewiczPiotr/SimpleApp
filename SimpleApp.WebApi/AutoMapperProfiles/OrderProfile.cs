using AutoMapper;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();

            CreateMap<ManageOrderDto, Order>()
                .ForMember(dest => dest.OrderItems, opt => opt.PreCondition(source => source.OrderItems != null))
                .ForMember(dest => dest.Status, opt => opt.PreCondition(source => source.Status != null));
        }
    }
}
