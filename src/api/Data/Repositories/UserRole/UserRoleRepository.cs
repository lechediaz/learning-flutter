using api.Data.Base;
using api.Models.Entities;

namespace api.Data.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole, ApplicationDbContext>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}