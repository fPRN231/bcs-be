namespace Domain.Models;

public partial class DoctorLogTime : BaseEntity 
{
    
    public Guid DoctorId { get; set; }

    public virtual User Doctor { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsAvailable { get; set; }
}