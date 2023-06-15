using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class User : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public String Name { get; set; }
        public String PhoneNumber { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public virtual Role Role { get; set; }
        public virtual DoctorInfo DoctorInfo { get; set; }
        public virtual ICollection<Bird> BirdsOwned { get; set; }
        public virtual ICollection<DoctorLogTime> DoctorLogTimes { get; set; }

    }
}
