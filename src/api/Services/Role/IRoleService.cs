using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.Dtos;
using api.Services.Base;

namespace api.Services
{
    public interface IRoleService
    {
        Task<ServiceResult<List<RoleDto>>> GetAllAsync();
        Task<ServiceResult<RoleDto>> GetByIdAsync(int id);
        Task<ServiceResult<RoleDto>> AddAsync(CreateRoleDto createRoleDto);
        Task<ServiceResult> UpdateAsync(UpdateRoleDto updateRoleDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> ValidateNameAsync(string name);
        Task<ServiceResult> ValidateNameAsync(string name, int ignoreId);
    }
}