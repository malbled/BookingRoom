using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Booking"/>
    /// </summary>
    public class BookingEntityTypeConfiguration : IEntityTypeConfiguration<Booking>
    {
        void IEntityTypeConfiguration<Booking>.Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.DateCheckIn).IsRequired();
            builder.Property(x => x.DateCheckout).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.RoomId).IsRequired();
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.GuestId).IsRequired();
            builder.Property(x => x.ServiceId).IsRequired();
        }
    }
}
