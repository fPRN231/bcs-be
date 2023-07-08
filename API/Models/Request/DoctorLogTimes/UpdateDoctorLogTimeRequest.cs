using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.DoctorLogTimes
{
    public class UpdateDoctorLogTimeRequest : IMapTo<DoctorLogTime>
    {
        public DateOnly StartTime { get; set; }

        public DateOnly EndTime { get; set; }

        public bool IsAvailable { get; set; }
    }
}
