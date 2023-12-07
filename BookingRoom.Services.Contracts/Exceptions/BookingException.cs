namespace BookingRoom.Services.Contracts.Exceptions
{
    public abstract class BookingException : Exception
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingException"/> без параметров
        /// </summary>
        protected BookingException() { }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingException"/> с указанием
        /// сообщения об ошибке
        /// </summary>
        protected BookingException(string message)
            : base(message) { }
    }
}
