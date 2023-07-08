using API.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Request.Services
{
    public class UpdateServiceRequest : IMapTo<Service>
    {
        public string Description { get; set; }

        public decimal BookingPrice { get; set; }
    }
}
