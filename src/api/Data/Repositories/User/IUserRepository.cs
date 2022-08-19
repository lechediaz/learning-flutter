using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data.Repositories.Base;
using api.Models.Dtos;
using api.Models.Entities;

namespace api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<UserDto>> GetWithRoleAsync();
        Task<UserDto> GetWithRoleAsync(int id);
    }
}