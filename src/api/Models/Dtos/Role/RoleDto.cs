using api.Models.Base;

namespace api.Models.Dtos
{
    public class RoleDto : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}