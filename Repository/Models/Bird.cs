using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Persistence.Models;

namespace Repository.Models
{
    public class Bird
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BirdId { get; set; }
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
        public virtual ICollection<MedicalHistory> MedicalHistory { get; set; }
    }
}