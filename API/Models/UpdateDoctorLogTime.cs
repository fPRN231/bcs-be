using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UpdateDoctorLogTime : IMapTo<DoctorLogTime>
    {
        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public bool IsAvailable { get; set; }
    }
}
