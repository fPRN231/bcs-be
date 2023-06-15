using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class Service
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceId { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public decimal BookingPrice { get; set; }
    }
}