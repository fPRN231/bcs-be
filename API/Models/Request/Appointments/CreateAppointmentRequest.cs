using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class CreateAppointmentRequest : IMapTo<Appointment>
    {
        [Required]
        public Guid BirdId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public Guid DoctorLogTimeId { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
