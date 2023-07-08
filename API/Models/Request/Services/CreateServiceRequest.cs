using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Services
{
    public class CreateServiceRequest : IMapTo<Service>
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal BookingPrice { get; set; }
    }
}
