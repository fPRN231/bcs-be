using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class CreateMedicalHistoryRequest : IMapTo<MedicalHistory>
    {
        [Required]
        public Guid BirdId { get; set; }

        [Required]
        public string MedicalHistoryName { get; set; }
    }
}
