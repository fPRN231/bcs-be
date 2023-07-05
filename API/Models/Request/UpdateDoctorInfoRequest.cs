using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class UpdateDoctorInfoRequest : IMapTo<DoctorInfo>
    {
        public int YearsOfExperience { get; set; }
    }
}
