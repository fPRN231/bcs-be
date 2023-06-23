﻿using Persistence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models
{
    public partial class Feedback : BaseEntity
    {
        
        public int DoctorRating { get; set; }

        public string Comment { get; set; }

        public string AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
