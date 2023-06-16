using Persistence.Models;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public partial class DoctorLogTime : BaseEntity 
    {
        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool IsAvailable { get; set; }
    }
}