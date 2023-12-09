namespace BookingRoom.Services.Contracts.Exceptions
{
    public class BookingEntityNotFoundException<TEntity> : BookingNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingEntityNotFoundException{TEntity}"/>
        /// </summary>
        public BookingEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
