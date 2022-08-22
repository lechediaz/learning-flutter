using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories;
using api.Models.Dtos;
using api.Models.Entities;
using api.Services.Base;
using AutoMapper;

namespace api.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IMapper mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResult<UserRoleDto>> AddAsync(CreateUserRoleDto createUserRoleDto)
        {
            var result = new ServiceResult<UserRoleDto>();

            if (createUserRoleDto is null)
            {
                result.Message = "Information not supplied";
                return result;
            }

            UserRole userRole = mapper.Map<UserRole>(createUserRoleDto);

            ServiceResult userRoleValidation = await ValidateUserRoleAsync(userRole.UserId, userRole.RoleId);

            if (!userRoleValidation.Ok)
            {
                result.Message = userRoleValidation.Message;
                return result;
            }

            try
            {
                await userRoleRepository.AddAsync(userRole);

                result.Extras = mapper.Map<UserRoleDto>(userRole);
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var result = new ServiceResult();

            bool exists = await userRoleRepository.ExistsAsync(p => p.Id == id);

            if (!exists)
            {
                result.Message = "UserRole not found.";
                return result;
            }

            try
            {
                await userRoleRepository.DeleteAsync(id);

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<List<UserRoleDto>>> GetAllAsync()
        {
            var result = new ServiceResult<List<UserRoleDto>>();

            try
            {
                List<UserRole> userRoles = await userRoleRepository.GetAsync(includeProperties: "Role,User");
                List<UserRoleDto> userRoleMapped = mapper.Map<List<UserRoleDto>>(userRoles);

                result.Extras = userRoleMapped;
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> ValidateUserRoleAsync(int userId, int roleId)
        {
            var result = new ServiceResult();

            try
            {
                bool exists = await userRoleRepository.ExistsAsync(p => p.UserId == userId && p.RoleId == roleId);

                if (exists)
                {
                    result.Message = "The user has this role assigned already.";
                    return result;
                }

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }
    }
}