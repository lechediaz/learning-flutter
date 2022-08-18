using api.Models.Base;

namespace api.Models.Entities
{
    public class UserRole : IHasId
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}