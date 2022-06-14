using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Users
{
    public interface IGetAllUsersService
    {
        Task<List<User>> GetAllAsync();
    }

    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly ApplicationDbContext context;

        public GetAllUsersService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.OrderBy(u => u.Name).ToListAsync();
        }
    }
}