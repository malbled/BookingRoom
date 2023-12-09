using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="BookingModel"/>
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Получить список всех <see cref="BookingModel"/>
        /// </summary>
        Task<IEnumerable<BookingModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="BookingModel"/> по идентификатору
        /// </summary>
        Task<BookingModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую бронь
        /// </summary>
        Task<BookingModel> AddAsync(BookingRequestModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую бронь
        /// </summary>
        Task<BookingModel> EditAsync(BookingRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую бронь
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
