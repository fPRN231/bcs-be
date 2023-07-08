using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.DoctorLogTimes
{
    public class CreateDoctorLogTimeRequest : IMapTo<DoctorInfo>
    {
        [Required]
        public DateOnly StartTime { get; set; }

        [Required]
        public DateOnly EndTime { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
