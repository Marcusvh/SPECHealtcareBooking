using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthcareBookingAPI.Configurations
{
    public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            // TPT table mapping
            builder.ToTable("Nurses");

            // Properties
            builder.Property(n => n.NursingLevel)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(n => n.AssignedDepartment)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(n => n.ShiftType)
                   .HasConversion<string>() // store enum as string
                   .IsRequired();

            builder.Property(n => n.YearsOfExperience)
                   .IsRequired();

        }
    }
}
