using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Booking"/>
    /// </summary>
    public interface IBookingRedRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Booking"/>
        /// </summary>
        Task<IReadOnlyCollection<Booking>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Booking"/> по идентификатору
        /// </summary>
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Booking"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
