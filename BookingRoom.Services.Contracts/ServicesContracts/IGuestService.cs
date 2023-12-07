using BookingRoom.Services.Contracts.Models;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="GuestModel"/>
    /// </summary>
    public interface IGuestService
    {
        /// <summary>
        /// Получить список всех <see cref="GuestModel"/>
        /// </summary>
        Task<IEnumerable<GuestModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="GuestModel"/> по идентификатору
        /// </summary>
        Task<GuestModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового постояльца
        /// </summary>
        Task<GuestModel> AddAsync(GuestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего постояльца
        /// </summary>
        Task<GuestModel> EditAsync(GuestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего постояльца
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
