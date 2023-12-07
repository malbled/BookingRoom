namespace BookingRoom.Services.Contracts.Models
{
    /// <summary>
    /// Модель номера
    /// </summary>
    public class RoomModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название номера
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Тип номера
        /// </summary>
        public string TypeRoom { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
