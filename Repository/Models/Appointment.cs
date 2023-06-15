namespace Repository.Models
{
    public partial class Appointment : BaseEntity
    {
        public String Date { get; set; }
        public String Time { get; set; }
        public String BirdId { get; set; }
        public String? CustomerOrGuestId { get; set; }
        public String? DoctorId { get; set; }
        public String? PrescriptionId { get; set; }
        public virtual AppointmentStatus AppointmentStatus { get; set; }
        public virtual User CustomerOrGuest { get; set; }
        public virtual User Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual ICollection<Service> Services { get; set; }

    }
}
