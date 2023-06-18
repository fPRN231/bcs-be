using Persistence.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public partial class Service : BaseEntity
    { 
        public string Description { get; set; }

        public decimal BookingPrice { get; set; }
    }
}