using BookingRoom.API.Models.CreateRequest;

namespace BookingRoom.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания номера
    /// </summary>
    public class RoomRequest : CreateRoomRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
