using System.ComponentModel.DataAnnotations;

namespace api.Models.Dtos
{
    public class CreateRoleDto
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
    }
}