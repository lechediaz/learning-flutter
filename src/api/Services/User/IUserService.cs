using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.Dtos;
using api.Services.Base;

namespace api.Services
{
    public interface IUserService
    {
        Task<ServiceResult<List<UserDto>>> GetAllAsync();
        Task<ServiceResult<UserDto>> GetByIdAsync(int id);
        Task<ServiceResult<UserDto>> AddAsync(CreateUserDto createUserDto);
        Task<ServiceResult> UpdateAsync(UpdateUserDto updateUserDto);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> ValidateUserNameAsync(string userName);
        Task<ServiceResult> ValidateUserNameAsync(string userName, int ignoreId);
    }
}