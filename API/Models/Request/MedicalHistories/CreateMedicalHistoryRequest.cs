using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.MedicalHistories
{
    public class CreateMedicalHistoryRequest : IMapTo<MedicalHistory>
    { 
        [Required]
        public string MedicalHistoryName { get; set; }
    }
}
