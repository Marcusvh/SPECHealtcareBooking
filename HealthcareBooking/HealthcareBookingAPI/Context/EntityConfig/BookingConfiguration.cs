using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.BookingId);
        builder.Property(b => b.BookingId)
            .ValueGeneratedOnAdd();

        builder.Property(b => b.PatientNotes)
            .HasMaxLength(500);

        builder.Property(b => b.StaffNotes)
            .HasMaxLength(500);

        builder.Property(b => b.StartTime)
            .IsRequired();

        // Enums as string
        builder.Property(b => b.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.BookingCheckStage)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        // These look swapped but I will configure them as-is:
        builder.Property(b => b.StaffConfirmedAt)
            .IsRequired()
            .HasPrecision(0);

        builder.Property(b => b.ConfirmedByStaffId)
            .IsRequired();

        // Auditing
        builder.Property(b => b.CreatedAt)
            .HasPrecision(0)
            .IsRequired();

        builder.Property(b => b.UpdatedAt);

        // ----------------------------
        // Relationships
        // ----------------------------

        // Booking → BookingType (many-to-one)
        builder.HasOne(b => b.BookingType)
               .WithMany(bt => bt.Bookings)
               .HasForeignKey(b => b.BookingTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        // Booking → Patient
        builder.HasOne(b => b.Patient)
               .WithMany()
               .HasForeignKey(b => b.PatientId)
               .OnDelete(DeleteBehavior.Restrict);

        // Booking → Staff
        builder.HasOne(b => b.Staff)
               .WithMany()
               .HasForeignKey(b => b.StaffId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
