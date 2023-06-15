using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class DoctorLogTime
    {
        [Required]
        [Key]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public bool IsAvailable { get; set; }
    }
}