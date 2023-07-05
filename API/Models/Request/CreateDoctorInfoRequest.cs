using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class CreateDoctorInfoRequest : IMapTo<DoctorInfo>
    {
        [Required]
        public int YearsOfExperience { get; set; }

    }
}
