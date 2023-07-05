using System.Text.Json.Serialization;

namespace Domain.Models;

public partial class DoctorInfo : BaseEntity
{
    public DoctorInfo()
    {
        Qualifications = new HashSet<Qualification>();
    }

    public string DoctorId { get; set; }

    public int YearsOfExperience { get; set; }

    public int Rating { get; set; }

    public virtual User Doctor { get; set; }

    [JsonIgnore]
    public virtual ICollection<Qualification> Qualifications { get; set; }
}