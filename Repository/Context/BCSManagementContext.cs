using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Models;

namespace Repository.Context;

public class BCSManagementContext : DbContext
{
    public BCSManagementContext()
    {
    }

    public BCSManagementContext(DbContextOptions<BCSManagementContext> options) : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<Bird> Birds { get; set; }
    public virtual DbSet<DoctorInfo> DoctorInfos { get; set; }
    public virtual DbSet<DoctorLogTime> DoctorLogTimes { get; set; }
    public virtual DbSet<Feedback> Feedbacks { get; set; }
    public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorLogTime>(builder =>
        {
            builder.Property(x => x.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(x => x.Time)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
        });
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.Property(x => x.Date)
                .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            builder.Property(x => x.Time)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
        });

        modelBuilder.Entity<Prescription>()
            .HasOne(p => p.Appointment)
            .WithMany()
            .HasForeignKey(p => p.AppointmentId);
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Prescription)
            .WithOne()
            .HasForeignKey<Appointment>(a => a.PrescriptionId);
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.CustomerOrGuest)
            .WithMany()
            .HasForeignKey(a => a.CustomerOrGuestId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Prescription)
            .WithMany()
            .HasForeignKey(a => a.PrescriptionId)
            .OnDelete(DeleteBehavior.Restrict);
        base.OnModelCreating(modelBuilder);

    }
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConverter() : base(
                dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                dateTime => DateOnly.FromDateTime(dateTime))
        {
        }
    }

    public class DateOnlyComparer : ValueComparer<DateOnly>
    {
        public DateOnlyComparer() : base(
            (d1, d2) => d1.DayNumber == d2.DayNumber,
            d => d.GetHashCode())
        {
        }
    }
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        public TimeOnlyConverter() : base(
                timeOnly => timeOnly.ToTimeSpan(),
                timeSpan => TimeOnly.FromTimeSpan(timeSpan))
        {
        }
    }

    public class TimeOnlyComparer : ValueComparer<TimeOnly>
    {
        public TimeOnlyComparer() : base(
            (t1, t2) => t1.Ticks == t2.Ticks,
            t => t.GetHashCode())
        {
        }
    }
}
