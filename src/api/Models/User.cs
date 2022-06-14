

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(60)]
        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        [Required]
        public string UserName { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }
    }
}