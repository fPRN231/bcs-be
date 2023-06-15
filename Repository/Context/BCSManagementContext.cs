using Microsoft.EntityFrameworkCore;
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<DoctorLogTime>()
            .Ignore(d => d.Date);
        modelBuilder.Entity<DoctorLogTime>()
            .Ignore(t => t.Time);
        modelBuilder.Entity<Appointment>()
            .Ignore(d => d.Date);
        modelBuilder.Entity<Appointment>()
            .Ignore(t => t.Time);
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

    }
}
