using Persistence.Models;
using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public partial class DoctorLogTime : BaseEntity 
    {
        
        public string DoctorId { get; set; }

        public virtual User Doctor { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool IsAvailable { get; set; }
    }
}