using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookingRoom.Context.Contracts.Configuration.Configurations
{
    /// <summary>
    /// Конфигурация для <see cref="Staff"/>
    /// </summary>
    public class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        void IEntityTypeConfiguration<Staff>.Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.HasIndex(x => x.Post).HasDatabaseName($"{nameof(Staff)}_{nameof(Staff.Post)}")
                .HasFilter($"{nameof(Staff.DeletedAt)} is null");
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MiddleName).HasMaxLength(50).IsRequired();
            builder.HasMany(x => x.Bookings).WithOne(x => x.Staff).HasForeignKey(x => x.StaffId);
        }
    }
}
