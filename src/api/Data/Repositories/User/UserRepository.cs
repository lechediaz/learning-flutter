using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Repositories.Base;
using api.Models.Dtos;
using api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, ApplicationDbContext>, IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<UserDto>> GetWithRoleAsync()
        {
            IQueryable<UserDto> query = from u in dbContext.Users
                                        join ur in dbContext.UserRoles
                                            on u.Id equals ur.UserId into possibleUserRole
                                        from pur in possibleUserRole.DefaultIfEmpty()
                                        join r in dbContext.Roles
                                            on pur.RoleId equals r.Id into possibleRole
                                        from pr in possibleRole.DefaultIfEmpty()
                                        orderby u.Name
                                        select new UserDto()
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            UserName = u.UserName,
                                            RoleName = pr.Name
                                        };

            List<UserDto> users = await query.ToListAsync();

            return users;
        }

        public async Task<UserDto> GetWithRoleAsync(int id)
        {
            IQueryable<UserDto> query = from u in dbContext.Users
                                        join ur in dbContext.UserRoles
                                            on u.Id equals ur.UserId into possibleUserRole
                                        from pur in possibleUserRole.DefaultIfEmpty()
                                        join r in dbContext.Roles
                                            on pur.RoleId equals r.Id into possibleRole
                                        from pr in possibleRole.DefaultIfEmpty()
                                        where u.Id == id
                                        select new UserDto()
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            UserName = u.UserName,
                                            RoleName = pr.Name
                                        };

            UserDto user = await query.FirstOrDefaultAsync();

            return user;
        }
    }
}