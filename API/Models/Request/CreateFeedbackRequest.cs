using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class CreateFeedbackRequest : IMapTo<Feedback>
    {
        [Required]
        public int DoctorRating { get; set; }

        public string Comment { get; set; }

    }
}
