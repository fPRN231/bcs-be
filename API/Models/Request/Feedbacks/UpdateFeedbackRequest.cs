using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Feedbacks
{
    public class UpdateFeedbackRequest : IMapTo<Feedback>
    {
        public int DoctorRating { get; set; }

        public string Comment { get; set; }

    }
}
