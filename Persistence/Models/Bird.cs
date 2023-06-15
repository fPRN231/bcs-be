namespace Repository.Models
{
    public partial class Bird: BaseEntity
    {
        public String Name { get; set; }
        public List<String> MedicalHistory { get; set; }
        public String Species { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public String UserId { get; set; }
        public virtual User User { get; set; }
    }
}