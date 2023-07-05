using Domain.Constants;
using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Appointment : BaseEntity
{
    public Appointment()
    {
        Services = new HashSet<Service>();
    }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }
    public string Prescription { get; set; }

    public string BirdId { get; set; }

    public string DoctorId { get; set; }

    public virtual User Doctor { get; set; }

    public virtual AppointmentStatus AppointmentStatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<Service> Services { get; set; }
}
