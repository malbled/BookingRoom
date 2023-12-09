using BookingRoom.Services.Contracts.Models;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="RoomModel"/>
    /// </summary>
    public interface IRoomService
    {
        /// <summary>
        /// Получить список всех <see cref="RoomModel"/>
        /// </summary>
        Task<IEnumerable<RoomModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="RoomModel"/> по идентификатору
        /// </summary>
        Task<RoomModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый номер
        /// </summary>
        Task<RoomModel> AddAsync(RoomModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий номер
        /// </summary>
        Task<RoomModel> EditAsync(RoomModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий номер
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
