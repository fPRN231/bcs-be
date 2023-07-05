using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UpdateDoctorInfoRequest : IMapTo<DoctorInfo>
    {
        public int YearsOfExperience { get; set; }
    }
}
