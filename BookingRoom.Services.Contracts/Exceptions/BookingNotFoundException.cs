namespace BookingRoom.Services.Contracts.Exceptions
{
    public class BookingNotFoundException : BookingException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingNotFoundException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        public BookingNotFoundException(string message)
            : base(message)
        { }
    }
}
