using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Context;

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
    //public virtual DbSet<Prescription> Prescriptions { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Convert DateOnly & TimeOnly
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
        #endregion

        base.OnModelCreating(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Feedback>(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);

    }

    #region Classes For DateOnly & TimeOnly
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
    #endregion
}
