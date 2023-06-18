using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public partial class MedicalHistory : BaseEntity
    {
        
        public string MedicalHistoryName { get; set;}
        
        public string BirdId { get; set; }

        public virtual Bird Bird { get; set; }
    }
}
