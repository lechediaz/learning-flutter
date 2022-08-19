using api.Models.Base;

namespace api.Models.Dtos
{
    public class UpdateRoleDto : CreateRoleDto, IHasId
    {
        public int Id { get; set; }
    }
}