using BookingRoom.Services.Contracts.Models;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="HotelModel"/>
    /// </summary>
    public interface IHotelService
    {
        /// <summary>
        /// Получить список всех <see cref="HotelModel"/>
        /// </summary>
        Task<IEnumerable<HotelModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="HotelModel"/> по идентификатору
        /// </summary>
        Task<HotelModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый отель
        /// </summary>
        Task<HotelModel> AddAsync(HotelModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующий отель
        /// </summary>
        Task<HotelModel> EditAsync(HotelModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующий отель
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
