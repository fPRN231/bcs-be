using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.DoctorLogTimes
{
    public class CreateDoctorLogTimeRequest : IMapTo<DoctorLogTime>
    {
        [Required, Range(0, 23)]
        public double StartTime { get; set; }

        [Required, Range(0, 23)]
        public double EndTime { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required, Range((int) DayOfWeek.Sunday, (int) DayOfWeek.Saturday)]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
