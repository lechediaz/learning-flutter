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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IMapper mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        private ServiceResult ValidateFields(Role role)
        {
            var result = new ServiceResult();

            if (role == null || (role != null && string.IsNullOrEmpty(role?.Name)))
            {
                result.Message = "Information not supplied.";
                return result;
            }

            if (role.Name.Length > 60)
            {
                result.Message = "The Name can have 60 characters only.";
                return result;
            }

            result.Ok = true;

            return result;
        }

        public async Task<ServiceResult<RoleDto>> AddAsync(CreateRoleDto createRoleDto)
        {
            var result = new ServiceResult<RoleDto>();

            if (createRoleDto is null)
            {
                result.Message = "Information not supplied";
                return result;
            }

            Role role = mapper.Map<Role>(createRoleDto);
            ServiceResult fieldsValidation = ValidateFields(role);

            if (!fieldsValidation.Ok)
            {
                result.Message = fieldsValidation.Message;
                return result;
            }

            ServiceResult nameValidation = await ValidateNameAsync(role.Name);

            if (!nameValidation.Ok)
            {
                result.Message = nameValidation.Message;
                return result;
            }

            try
            {
                await roleRepository.AddAsync(role);

                result.Extras = mapper.Map<RoleDto>(role);
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

            bool exists = await roleRepository.ExistsAsync(p => p.Id == id);

            if (!exists)
            {
                result.Message = "Role not found.";
                return result;
            }

            try
            {
                await roleRepository.DeleteAsync(id);

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<List<RoleDto>>> GetAllAsync()
        {
            var result = new ServiceResult<List<RoleDto>>();

            try
            {
                List<Role> roles = await roleRepository.GetAsync();
                List<RoleDto> rolesMapped = mapper.Map<List<RoleDto>>(roles);

                result.Extras = rolesMapped;
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult<RoleDto>> GetByIdAsync(int id)
        {
            var result = new ServiceResult<RoleDto>();

            try
            {
                Role role = await roleRepository.GetAsync(id);
                RoleDto roleMapped = mapper.Map<RoleDto>(role);

                result.Extras = roleMapped;
                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> UpdateAsync(UpdateRoleDto updateRoleDto)
        {
            var result = new ServiceResult();

            if (updateRoleDto is null)
            {
                result.Message = "Information not supplied";
                return result;
            }

            bool exists = await roleRepository.ExistsAsync(p => p.Id == updateRoleDto.Id);

            if (!exists)
            {
                result.Message = "Role not found.";
                return result;
            }

            Role role = await roleRepository.GetAsync(updateRoleDto.Id);

            role = mapper.Map(updateRoleDto, role);

            ServiceResult fieldsValidation = ValidateFields(role);

            if (!fieldsValidation.Ok)
            {
                result.Message = fieldsValidation.Message;
                return result;
            }

            ServiceResult roleNameValidation = await ValidateNameAsync(role.Name, role.Id);

            if (!roleNameValidation.Ok)
            {
                result.Message = roleNameValidation.Message;
                return result;
            }

            try
            {
                await roleRepository.UpdateAsync(role);

                result.Ok = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<ServiceResult> ValidateNameAsync(string name)
        {
            var result = new ServiceResult();

            try
            {
                name = name.ToLower();

                bool exists = await roleRepository.ExistsAsync(p => p.Name.ToLower() == name);

                if (exists)
                {
                    result.Message = "The Name was taken by other role.";
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

        public async Task<ServiceResult> ValidateNameAsync(string name, int ignoreId)
        {
            var result = new ServiceResult();

            try
            {
                name = name.ToLower();

                bool exists = await roleRepository.ExistsAsync(p =>
                    p.Id != ignoreId && p.Name.ToLower() == name);

                if (exists)
                {
                    result.Message = "The Name was taken by other role.";
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