using Persistence.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Persistence.Models;

namespace Repository.Models
{
    public partial class Appointment : BaseEntity
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public string BirdId { get; set; }

        [Required]
        public string CustomerOrGuestId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual AppointmentStatus? AppointmentStatus { get; set; }

        public string PrescriptionId { get; set; }

        public virtual User CustomerOrGuest { get; set; }

        public virtual User User { get; set; }

        public virtual Prescription Prescription { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
    }
}
