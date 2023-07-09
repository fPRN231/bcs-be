using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class SelectAppointmentServices : IMapTo<Appointment>
    {
        [Required]
        public virtual ICollection<Service> Services { get; set; }
    }
}
