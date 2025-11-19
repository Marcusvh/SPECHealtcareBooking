using HealthcareModels.Models;
using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        // Base table
        builder.ToTable("Staff");

        builder.HasKey(s => s.StaffId);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.Type)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(s => s.SupportedBookingTypes)
       .WithMany(b => b.StaffMembers)
       .UsingEntity<Dictionary<string, object>>(
           "StaffBookingType",
           j => j.HasOne<BookingType>().WithMany().HasForeignKey("BookingTypeId"),
           j => j.HasOne<Staff>().WithMany().HasForeignKey("StaffId")
       );

    }
}
