using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Guest"/>
    /// </summary>
    public interface IGuestRedRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Guest"/>
        /// </summary>
        Task<IReadOnlyCollection<Guest>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Guest"/> по идентификатору
        /// </summary>
        Task<Guest?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Guest"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Guest>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Guest"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
