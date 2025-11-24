using HealthcareBookingAPI.Configurations;
using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;

namespace HealthcareBookingAPI.Context
{
    public class HealthcareContext : DbContext
    {
        public HealthcareContext(DbContextOptions<HealthcareContext> options) : base(options) {}

        // PATIENT
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Location> Locations { get; set; }

        // Start STAFF
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<MedicalStudent> MedicalStudents { get; set; }

        // BOOKING
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingType> BookingTypes { get; set; }

        // NOTIFY
        public DbSet<NotifyStaff> NotifyStaffs { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HavePrecision(0);
            base.ConfigureConventions(configurationBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PATIENT
            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());

            // STAFF
            modelBuilder.ApplyConfiguration(new StaffConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new NurseConfiguration());
            modelBuilder.ApplyConfiguration(new MedicalStudentConfiguration());

            // BOOKING
            modelBuilder.ApplyConfiguration(new BookingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            // NOTIFY
            modelBuilder.ApplyConfiguration(new NotifyStaffConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
