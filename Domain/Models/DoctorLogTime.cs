namespace Domain.Models;

public partial class DoctorLogTime : BaseEntity 
{
    
    public Guid DoctorId { get; set; }

    public virtual User Doctor { get; set; }

    public double StartTime { get; set; }

    public double EndTime { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public bool IsAvailable { get; set; }
}