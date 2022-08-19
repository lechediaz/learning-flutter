using System.ComponentModel.DataAnnotations;

namespace api.Models.Dtos
{
    public class CreateUserDto
    {
        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required]
        public string UserName { get; set; }
    }
}