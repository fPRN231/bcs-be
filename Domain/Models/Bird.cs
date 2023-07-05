using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class Bird : BaseEntity
{
    public Bird()
    {
        MedicalHistories = new HashSet<MedicalHistory>();
        Appointments = new HashSet<Appointment>();
    }

    public string Name { get; set; }

    public string Species { get; set; }

    public int Age { get; set; }

    public bool Gender { get; set; }

    public string UserId { get; set; }

    public virtual User User { get; set; }

    [JsonIgnore]
    public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }

    [JsonIgnore]
    public virtual ICollection<Appointment> Appointments { get; set; }
}