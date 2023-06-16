using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Persistence.Constants;
using Persistence.Models;

namespace Repository.Models
{
    public partial class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public virtual Role Role { get; set; }

        public virtual DoctorInfo DoctorInfo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bird> BirdsOwned { get; set; }

        [JsonIgnore]
        public virtual ICollection<DoctorLogTime> DoctorLogTimes { get; set; }
    }
}
