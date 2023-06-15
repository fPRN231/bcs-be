using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class Feedback : BaseEntity
    {
        public int DoctorRating { get; set; }
        public String Comment { get; set; }
        public String DoctorId { get; set; }
        public virtual User Doctor { get; set; }
    }
}
