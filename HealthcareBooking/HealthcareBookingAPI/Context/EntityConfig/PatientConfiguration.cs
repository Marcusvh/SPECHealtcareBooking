using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(p => p.PatientId);

        builder.Property(p => p.PatientId)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Phone)
            .HasMaxLength(20);

        builder.Property(p => p.Address)
            .HasMaxLength(250);

        builder.Property(p => p.DateOfBirth)
            .HasPrecision(0);

        builder.Property(p => p.PreferredContactMethod)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        // Relationship to Location
        builder
            .HasOne(p => p.Location)
            .WithMany()                         // If Location has many Patients
            .HasForeignKey(p => p.LocationId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade deletes
    }
}
