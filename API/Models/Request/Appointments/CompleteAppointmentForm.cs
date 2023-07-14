using API.Mappings;
using Domain.Constants;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Appointments
{
    public class CompleteAppointmentForm : IMapTo<Appointment>
    { 
        [Required]
        public Guid BirdId { get; set; }

        public string Prescription { get; set; }
    }
}
