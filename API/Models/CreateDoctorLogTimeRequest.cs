using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreateDoctorLogTimeRequest : IMapTo<DoctorInfo>
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
