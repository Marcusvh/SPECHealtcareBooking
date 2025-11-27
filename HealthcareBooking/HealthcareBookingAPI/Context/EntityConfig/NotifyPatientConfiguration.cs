using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NotifyPatientConfiguration : IEntityTypeConfiguration<NotifyPatient>
{
    public void Configure(EntityTypeBuilder<NotifyPatient> builder)
    {
        // Table name
        builder.ToTable("NotifyPatients");

        // Primary key
        builder.HasKey(n => n.PatientNotificationId);

        // Properties
        builder.Property(n => n.PatientNotificationId)
            .ValueGeneratedOnAdd();

        builder.Property(n => n.BookingId)
            .IsRequired();

        builder.Property(n => n.PatientId)
            .IsRequired();

        builder.Property(n => n.CreatedAt)
            .IsRequired();

        builder.Property(n => n.Subject)
            .HasMaxLength(200);

        // Enum conversions (stored as strings for readability)
        builder.Property(n => n.ContactChannelUsed)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(n => n.NotificationStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(n => n.NotificationReason)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(n => n.PatientId);
        builder.HasIndex(n => n.BookingId);
        builder.HasIndex(n => n.NotificationReason);
    }
}
