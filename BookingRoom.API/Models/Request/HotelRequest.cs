using BookingRoom.API.Models.CreateRequest;

namespace BookingRoom.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания отеля
    /// </summary>
    public class HotelRequest : CreateHotelRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
