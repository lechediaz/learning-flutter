using api.Models.Base;

namespace api.Models.Dtos
{
    public class UpdateUserDto : CreateUserDto, IHasId
    {
        public int Id { get; set; }
    }
}