using Persistence.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Persistence.Models;

namespace Repository.Models
{
    public partial class Appointment : BaseEntity
    {
        public Appointment()
        {
            Prescriptions = new HashSet<Prescription>();
            Services = new HashSet<Service>();
        }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public string BirdId { get; set; }

        public string CustomerOrGuestId { get; set; }
        public virtual User CustomerOrGuest { get; set; }

        public string DoctorId { get; set; }
        public virtual User Doctor { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }

        public string PrescriptionId { get; set; }

        [JsonIgnore]
        public virtual ICollection<Prescription> Prescriptions { get; set; }

        [JsonIgnore]
        public virtual ICollection<Service> Services { get; set; }
    }
}
