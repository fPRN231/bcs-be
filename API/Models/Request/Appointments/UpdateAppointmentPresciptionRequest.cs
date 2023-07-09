using API.Mappings;
using Domain.Models;

namespace API.Models.Request.Appointments
{
    public class UpdateAppointmentPresciptionRequest : IMapTo<Appointment>
    {
        public string? Prescription { get; set; }
    }
}
