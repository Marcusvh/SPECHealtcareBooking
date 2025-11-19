using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class MedicalStudentConfiguration : IEntityTypeConfiguration<MedicalStudent>
{
    public void Configure(EntityTypeBuilder<MedicalStudent> builder)
    {
        // TPT table mapping
        builder.ToTable("MedicalStudents");

        // Properties
        builder.Property(ms => ms.University)
                .HasMaxLength(200)
                .IsRequired();

        builder.Property(ms => ms.YearOfStudy)
                .IsRequired();

        builder.Property(ms => ms.SupervisorId)
                .IsRequired();

        builder.Property(ms => ms.InternshipStartDate)
                .HasColumnType("date")
                .IsRequired();

        builder.Property(ms => ms.InternshipEndDate)
                .HasColumnType("date")
                .IsRequired();

        builder.HasOne<Doctor>()
               .WithMany()
               .HasForeignKey(ms => ms.SupervisorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
