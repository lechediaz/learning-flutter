using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Models.Base;

namespace api.Models.Entities
{
    public class Role : IHasId
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
    }
}