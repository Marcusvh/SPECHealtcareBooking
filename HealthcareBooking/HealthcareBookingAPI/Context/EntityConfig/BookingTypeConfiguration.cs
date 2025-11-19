using HealthcareModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingTypeConfiguration : IEntityTypeConfiguration<BookingType>
{
    public void Configure(EntityTypeBuilder<BookingType> builder)
    {
        builder.ToTable("BookingTypes");

        builder.HasKey(bt => bt.BookingTypeId);

        builder.Property(bt => bt.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(bt => bt.Description)
            .HasMaxLength(300);
    }
}