using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assesment.Infrastructure.Identity;
using Assesment.Domain.Entities;

namespace Assesment.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.Id);
            
                entity.HasData(SeedDoctors.GetInitialDoctors());
            });

            builder.Entity<Appointment>()
                .HasOne(d => d.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            builder.Entity<Appointment>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(a => a.PatientId);
        }
    }
}