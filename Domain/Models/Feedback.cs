﻿namespace Domain.Models;

public partial class Feedback : BaseEntity
{
    
    public int DoctorRating { get; set; }

    public string Comment { get; set; }

    public Guid UserId { get; set; }

    public Guid AppointmentId { get; set; }

    public virtual User User { get; set; }

    public virtual Appointment Appointment { get; set; }
}
