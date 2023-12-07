using BookingRoom.Services.Contracts.Models;

namespace BookingRoom.Services.Contracts.ServicesContracts
{
    /// <summary>
    /// Сервис <see cref="StaffModel"/>
    /// </summary>
    public interface IStaffService
    {
        /// <summary>
        /// Получить список всех <see cref="StaffModel"/>
        /// </summary>
        Task<IEnumerable<StaffModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="StaffModel"/> по идентификатору
        /// </summary>
        Task<StaffModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового сотрудника
        /// </summary>
        Task<StaffModel> AddAsync(StaffModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует существующего сотрудника
        /// </summary>
        Task<StaffModel> EditAsync(StaffModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет существующего сотрудника
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
