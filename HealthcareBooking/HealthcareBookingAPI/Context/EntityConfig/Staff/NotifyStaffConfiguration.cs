using HealthcareModels.Models.HealthcareStaff;
using Microsoft.EntityFrameworkCore;
public class NotifyStaffConfiguration : IEntityTypeConfiguration<NotifyStaff>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<NotifyStaff> builder)
    {
        // Map to separate table
        builder.ToTable("NotifyStaffs");

        builder.HasKey(ns => ns.NotifyStaffId);
        builder.Property(ns => ns.NotifyStaffId)
            .ValueGeneratedOnAdd();

        builder.Property(m => m.StaffId)
            .IsRequired();

        builder.Property(m => m.RelatedBookingId)
            .IsRequired();

        builder.Property(ns => ns.CreatedAt)
            .IsRequired();
            
        builder.Property(ns => ns.NotificationStatus)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(ns => ns.NotificationType)
            .HasConversion<string>()
            .IsRequired();

        // Relationships
        builder
            .HasOne(ns => ns.Staff)
            .WithMany(s => s.NotifyStaffs)
            .HasForeignKey(ns => ns.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(ns => ns.StaffId);

    }
}