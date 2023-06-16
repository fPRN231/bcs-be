using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Persistence.Models;
using System.Text.Json.Serialization;

namespace Repository.Models
{
    public partial class Bird : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<MedicalHistory> MedicalHistory { get; set; }
    }
}