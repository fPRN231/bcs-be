using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.MedicalHistories
{
    public class UpdateMedicalHistoryRequest : IMapTo<MedicalHistory>
    {
        public string MedicalHistoryName { get; set; }
    }
}
