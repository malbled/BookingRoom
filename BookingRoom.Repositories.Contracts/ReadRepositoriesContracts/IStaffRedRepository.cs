using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Contracts.ReadRepositoriesContracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Staff"/>
    /// </summary>
    public interface IStaffRedRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Staff"/>
        /// </summary>
        Task<IReadOnlyCollection<Staff>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Staff"/> по идентификатору
        /// </summary>
        Task<Staff?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Staff"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Staff>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Проверить есть ли <see cref="Staff"/> в коллеции
        /// </summary>
        Task<bool> IsNotNullAsync(Guid id, CancellationToken cancellationToken);
    }
}
