using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // Map to separate table
        builder.ToTable("Doctors");

        // Map base properties via TPT
        builder.HasBaseType<Staff>();

        // Doctor-specific properties
        builder.Property(d => d.Specialties)
            .HasMaxLength(250);

        builder.Property(d => d.MedicalLincenseNumber)
            .HasMaxLength(50);

        builder.Property(d => d.AssignedDepartment)
            .HasMaxLength(100);

        builder.Property(d => d.YearsOfExperience)
            .IsRequired();

        builder.Property(d => d.IsAcceptingNewPatients)
            .IsRequired();
    }
}
