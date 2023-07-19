using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.DoctorLogTimes
{
    public class UpdateDoctorLogTimeRequest : IMapTo<DoctorLogTime>
    {
        [Range(0, 23)]
        public double StartTime { get; set; }

        [Range(0, 23)]
        public double EndTime { get; set; }

        public bool IsAvailable { get; set; }

        [Range((int)DayOfWeek.Sunday, (int)DayOfWeek.Saturday)]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
