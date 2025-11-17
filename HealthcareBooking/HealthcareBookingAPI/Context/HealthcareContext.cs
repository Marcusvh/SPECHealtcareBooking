using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Context
{
    public class HealthcareContext : DbContext
    {
        public HealthcareContext(DbContextOptions<HealthcareContext> options) : base(options) {}

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Location> Locations { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
