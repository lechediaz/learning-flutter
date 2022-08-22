using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.Dtos;
using api.Services.Base;

namespace api.Services
{
    public interface IUserRoleService
    {
        Task<ServiceResult<List<UserRoleDto>>> GetAllAsync();
        Task<ServiceResult<UserRoleDto>> AddAsync(CreateUserRoleDto createUserRoleDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> ValidateUserRoleAsync(int userId, int roleId);
    }
}