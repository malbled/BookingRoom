namespace BookingRoom.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Интерфейс для сохранения БД
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения в бд
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
