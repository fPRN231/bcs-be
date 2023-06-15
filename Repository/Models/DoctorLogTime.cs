namespace Repository.Models
{
    public partial class DoctorLogTime : BaseEntity
    {
        public String UserId { get; set; }
        public virtual User Doctor { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public bool IsAvailable { get; set; }
    }
}