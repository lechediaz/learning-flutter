

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models.Base;

namespace api.Models.Entities
{
    public class User : IHasId
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