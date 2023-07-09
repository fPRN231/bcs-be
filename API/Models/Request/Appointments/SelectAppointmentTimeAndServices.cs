using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class SelectAppointmentTimeAndServices : IMapTo<Appointment>
    {
        [Required]
        public DateTime AppointmentDateTime { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
