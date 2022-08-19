using api.Data.Repositories.Base;
using api.Models.Entities;

namespace api.Data.Repositories
{
    public class RoleRepository : RepositoryBase<Role, ApplicationDbContext>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}