using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public partial class Service : BaseEntity
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal BookingPrice { get; set; }
    }
}