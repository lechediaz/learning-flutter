using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await context.Users.OrderBy(u => u.Name).ToListAsync();
        }
    }
}