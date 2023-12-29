using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Room"/>
    /// </summary>
    public class RoomEntityTypeConfiguration : IEntityTypeConfiguration<Room>
    {
        void IEntityTypeConfiguration<Room>.Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).HasMaxLength(50).IsRequired();
            builder.Property(x => x.TypeRoom).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.Title)
                .IsUnique()
                .HasDatabaseName($"{nameof(Room)}_{nameof(Room.Title)}")
                .HasFilter($"{nameof(Room.DeletedAt)} is null");
            builder.HasMany(x => x.Bookings).WithOne(x => x.Room).HasForeignKey(x => x.RoomId);
        }
    }
}
