using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Service"/>
    /// </summary>
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        void IEntityTypeConfiguration<Service>.Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Title).HasMaxLength(150).IsRequired();
            builder.HasIndex(x => x.Title)
                .IsUnique()
                .HasDatabaseName($"{nameof(Service)}_{nameof(Service.Title)}")
                .HasFilter($"{nameof(Service.DeletedAt)} is null");
            builder.HasMany(x => x.Bookings).WithOne(x => x.Service).HasForeignKey(x => x.ServiceId);
        }
    }
}
