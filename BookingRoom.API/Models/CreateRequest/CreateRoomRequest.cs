namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания номера
    /// </summary>
    public class CreateRoomRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Тип номера
        /// </summary>
        public string TypeRoom { get; set; } = string.Empty;
    }
}
