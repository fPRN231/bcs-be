using Persistence.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Repository.Models
{
    public class Appointment
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AppointmentId { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
        [Required]
        [ForeignKey("Bird")]
        public string BirdId { get; set; }
        [Required]
        [ForeignKey("CustomerOrGuest")]
        public string CustomerOrGuestId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("Prescription")]
        public string PrescriptionId { get; set; }
        public virtual AppointmentStatus? AppointmentStatus { get; set; }
        public virtual User CustomerOrGuest { get; set; }
        public virtual User User { get; set; }
        public virtual Prescription Prescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
    }
}
