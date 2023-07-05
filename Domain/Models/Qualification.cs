namespace Domain.Models;

public partial class Qualification : BaseEntity
{
    
    public string Name { get; set; }
    
    public string DoctorId { get; set; }

    public virtual User User { get; set; }  
}