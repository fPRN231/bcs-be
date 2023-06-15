using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Prescription
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PrescriptionId { get; set; }

        [Required]
        [ForeignKey("Appointment")]
        public string AppointmentId { get; set; }

        [Required]
        public string Diagnose { get; set; }

        [Required]
        public string Medication { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
