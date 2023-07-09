using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class CompleteAppointmentForm : IMapTo<Appointment>
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        public Guid BirdId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public virtual ICollection<Service> Services { get; set; }

        [Required]
        public virtual AppointmentStatus AppointmentStatus { get; set; }
    }
}
