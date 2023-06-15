namespace Repository.Models
{
    public partial class DoctorInfo : BaseEntity
    {
        public String UserId { get; set; }
        public List<String> Qualifications { get; set; }
        public int YearsOfExperience { get; set; }
        public int Rating { get; set; }
        public virtual User User { get; set; }
    }
}