using BookingRoom.Services.Contracts.Models;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="ServiceModel"/>
    /// </summary>
    public interface IServiceService
    {
        /// <summary>
        /// Получить список всех <see cref="ServiceModel"/>
        /// </summary>
        Task<IEnumerable<ServiceModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ServiceModel"/> по идентификатору
        /// </summary>
        Task<ServiceModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новую услугу
        /// </summary>
        Task<ServiceModel> AddAsync(ServiceModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующую услугу
        /// </summary>
        Task<ServiceModel> EditAsync(ServiceModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующую услугу
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
