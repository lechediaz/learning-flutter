using api.Data.Base;
using api.Models.Entities;

namespace api.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, ApplicationDbContext>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}