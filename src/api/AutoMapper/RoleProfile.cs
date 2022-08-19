using api.Models.Dtos;
using api.Models.Entities;
using AutoMapper;

namespace api.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
            CreateMap<Role, RoleDto>();
        }
    }
}