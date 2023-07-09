using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class SelectAppointmentDoctor : IMapTo<Appointment>
    {
        [Required]
        public Guid DoctorId { get; set; }
    }
}
