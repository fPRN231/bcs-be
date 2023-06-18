using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public partial class Prescription : BaseEntity
    {
        
        public string AppointmentId { get; set; }

        public string Diagnose { get; set; }

        public string Medication { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
