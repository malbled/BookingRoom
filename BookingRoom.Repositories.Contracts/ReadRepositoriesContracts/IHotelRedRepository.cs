using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Hotel"/>
    /// </summary>
    public interface IHotelRedRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Hotel"/>
        /// </summary>
        Task<IReadOnlyCollection<Hotel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hotel"/> по идентификатору
        /// </summary>
        Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Hotel"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Hotel>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Hotel"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
