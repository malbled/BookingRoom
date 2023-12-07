using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Common.Entity.Repositories;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Anchors;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using Microsoft.EntityFrameworkCore;

namespace BookingRoom.Repositories.ReadRepositories
{
    /// <summary>
    /// Реализация <see cref="IBookingRedRepository"/>
    /// </summary>
    public class BookingReadRepository : IBookingRedRepository, IRepositoryAnchor
    {

        /// <summary>
        /// Контекст для связи с бд
        /// </summary>
        private IDbRead reader;

        public BookingReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Booking>> IBookingRedRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Booking>()
                .NotDeletedAt()
                .OrderBy(x => x.DateCheckIn)
                .ThenBy(x => x.DateCheckout)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Booking?> IBookingRedRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Booking>()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<bool> IBookingRedRepository.IsNotNullAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Booking>().AnyAsync(x => x.Id == id && !x.DeletedAt.HasValue, cancellationToken);
    }
}
