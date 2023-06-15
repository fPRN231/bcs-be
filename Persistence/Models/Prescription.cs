namespace Repository.Models
{
    public partial class Prescription: BaseEntity
    {
        public String Diagnose { get; set; }
        public String Medication { get; set; }
        public String AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}