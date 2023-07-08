using Domain.Constants;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Appointment : BaseEntity
{
    public Appointment()
    {
        Services = new HashSet<Service>();
    }

    public DateTime AppointmentDateTime { get; set; }

    public string Prescription { get; set; }

    public Guid BirdId { get; set; }

    public Guid DoctorId { get; set; }

    public virtual User Doctor { get; set; }

    public virtual AppointmentStatus AppointmentStatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<Service> Services { get; set; }
}
