using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public partial class Prescription : BaseEntity
    {
        [Required]
        public string AppointmentId { get; set; }

        [Required]
        public string Diagnose { get; set; }

        [Required]
        public string Medication { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
