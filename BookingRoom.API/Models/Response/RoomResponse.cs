namespace BookingRoom.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности номера
    /// </summary>
    public class RoomResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Название
        /// </summary>
        public string TypeRoom { get; set; } = string.Empty;
    }
}
