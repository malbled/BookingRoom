using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Hotel"/>
    /// </summary>
    public class HotelEntityTypeConfiguration : IEntityTypeConfiguration<Hotel>
    {
        void IEntityTypeConfiguration<Hotel>.Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(60).IsRequired();
            builder.HasIndex(x => x.Title).HasDatabaseName($"{nameof(Hotel)}_{nameof(Hotel.Title)}");
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.HasIndex(x => x.Address).HasDatabaseName($"{nameof(Hotel)}_{nameof(Hotel.Address)}")
                .IsUnique()
                .HasFilter($"{nameof(Hotel.DeletedAt)} is null");
            builder.HasMany(x => x.Bookings).WithOne(x => x.Hotel).HasForeignKey(x => x.HotelId);
        }
    }
}
