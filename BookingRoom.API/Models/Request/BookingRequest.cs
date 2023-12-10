using BookingRoom.API.Models.CreateRequest;

namespace BookingRoom.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания билета
    /// </summary>
    public class BookingRequest : CreateBookingRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
