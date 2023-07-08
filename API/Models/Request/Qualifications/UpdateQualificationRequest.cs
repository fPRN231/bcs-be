using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Qualifications
{
    public class UpdateQualificationRequest : IMapTo<Qualification>
    {
        public string Name { get; set; }
    }
}
