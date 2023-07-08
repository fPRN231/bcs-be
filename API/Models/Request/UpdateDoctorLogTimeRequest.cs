using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class UpdateDoctorLogTimeRequest : IMapTo<DoctorLogTime>
    {
        public DateOnly LogDateTime { get; set; }

        public bool IsAvailable { get; set; }
    }
}
