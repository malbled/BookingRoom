namespace BookingRoom.Services.Contracts.Exceptions
{
    public class BookingInvalidOperationException : BookingException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingInvalidOperationException"/>
        /// с указанием сообщения об ошибке
        /// </summary>
        public BookingInvalidOperationException(string message)
            : base(message)
        {

        }
    }
}
