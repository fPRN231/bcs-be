using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class UpdateAppointmentRequest : IMapTo<Appointment>
    {
        public DateTime AppointmentDateTime { get; set; }

        public string Prescription { get; set; }

        public Guid BirdId { get; set; }

        public Guid DoctorId { get; set; }

        public virtual ICollection<Service> Services { get; set; }

        public virtual AppointmentStatus AppointmentStatus { get; set; }
    }
}
