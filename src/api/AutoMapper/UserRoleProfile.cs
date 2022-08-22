using api.Models.Dtos;
using api.Models.Entities;
using AutoMapper;

namespace api.AutoMapper
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<CreateUserRoleDto, UserRole>();
            CreateMap<UserRole, UserRoleDto>()
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.RoleName, options => options.MapFrom(src => src.Role.Name));
        }
    }
}