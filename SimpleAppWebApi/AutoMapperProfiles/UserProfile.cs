using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using AutoMapper;

namespace SimpleApp.WebApi.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterDto>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
