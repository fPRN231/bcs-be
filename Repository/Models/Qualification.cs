using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public partial class Qualification : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}