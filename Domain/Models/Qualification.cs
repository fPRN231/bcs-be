namespace Domain.Models;

public partial class Qualification : BaseEntity
{
    
    public string Name { get; set; }
    
    public Guid DoctorId { get; set; }

    public virtual User User { get; set; }  
}