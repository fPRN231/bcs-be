using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class CreateDoctorLogTimeRequest : IMapTo<DoctorInfo>
    {
        [Required]
        public DateOnly LogDateTime { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
