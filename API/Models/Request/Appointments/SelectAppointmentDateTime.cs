using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class SelectAppointmentDateTime : IMapTo<Appointment>
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }
    }
}
