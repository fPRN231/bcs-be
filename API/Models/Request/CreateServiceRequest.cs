using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request
{
    public class CreateServiceRequest : IMapTo<Service>
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal BookingPrice { get; set; }
    }
}
