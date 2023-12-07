using BookingRoom.Common.Entity.EntityInterface;

namespace BookingRoom.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Интерфейс записи в БД
    /// </summary>
    public interface IDbWriter
    {
        /// <summary>
        /// Добавить новую запись
        /// </summary>
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Изменить запись
        /// </summary>
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        /// <summary>
        /// Удалить запись
        /// </summary>
        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}
