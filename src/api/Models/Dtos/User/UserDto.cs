using api.Models.Base;

namespace api.Models.Dtos
{
    public class UserDto : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}