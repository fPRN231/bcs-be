using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class UpdateFeedbackRequest : IMapTo<Bird>
    {
        public int DoctorRating { get; set; }

        public string Comment { get; set; }

    }
}
