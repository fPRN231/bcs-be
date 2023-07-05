namespace Domain.Models;

public partial class MedicalHistory : BaseEntity
{
    
    public string MedicalHistoryName { get; set;}
    
    public Guid BirdId { get; set; }

    public virtual Bird Bird { get; set; }
}
