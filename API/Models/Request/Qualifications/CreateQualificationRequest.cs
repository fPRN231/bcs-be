using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Qualifications
{
    public class CreateQualificationRequest : IMapTo<Qualification>
    {
        [Required]
        public string Name { get; set; }
    }
}
