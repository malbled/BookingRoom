using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts;
using BookingRoom.Context.Contracts.Configuration.Configurations;
using BookingRoom.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Context
{
    /// <summary>
    /// Контекст работы с БД
    /// </summary>
    /// <remarks>
    /// 1) dotnet tool install --global dotnet-ef
    /// 2) dotnet tool update --global dotnet-ef
    /// 3) dotnet ef migrations add [name] --project BookingRoom.Context\BookingRoom.Context.csproj
    /// 4) dotnet ef database update --project BookingRoom.Context\BookingRoom.Context.csproj
    /// 5) dotnet ef database update [targetMigrationName] --BookingRoom.Context\BookingRoom.Context.csproj
    /// 
    ///  dotnet tool update --global dotnet-ef --version 6.0.0
    /// </remarks>
    public class BookingRoomContext : DbContext, IBookingRoomContext, IDbRead, IDbWriter, IUnitOfWork
    {
        public DbSet<Hotel> Hotels { get; }

        public DbSet<Guest> Guests { get; }

        public DbSet<Room> Rooms { get; }

        public DbSet<Staff> Staffs { get; }

        public DbSet<Service> Services { get; }

        public DbSet<Booking> Bookings { get; }

        public BookingRoomContext(DbContextOptions<BookingRoomContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelEntityTypeConfiguration).Assembly);
        }

        /// <summary>
        /// Сохранение изменений в БД
        /// </summary>
        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }
            return count;
        }

        /// <summary>
        /// Чтение сущностей из БД
        /// </summary>
        IQueryable<TEntity> IDbRead.Read<TEntity>()
            => base.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

        /// <summary>
        /// Запись сущности в БД
        /// </summary>
        void IDbWriter.Add<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        /// <summary>
        /// Обновление сущностей
        /// </summary>
        void IDbWriter.Update<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        /// <summary>
        /// Удаление сущности из БД
        /// </summary>
        void IDbWriter.Delete<TEntity>(TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;
    }
}
