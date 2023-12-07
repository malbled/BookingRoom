using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Guest"/>
    /// </summary>
    public class GuestEntityTypeConfiguration : IEntityTypeConfiguration<Guest>
    {
        void IEntityTypeConfiguration<Guest>.Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.ToTable("Guests");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AddressRegistration).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Passport).HasMaxLength(20).IsRequired();
            builder.HasIndex(x => x.Passport)
                .IsUnique()
                .HasDatabaseName($"{nameof(Guest)}_{nameof(Guest.Passport)}")
                .HasFilter($"{nameof(Guest.DeletedAt)} is null");
            builder.HasMany(x => x.Bookings).WithOne(x => x.Guest).HasForeignKey(x => x.GuestId);
        }
    }
}
