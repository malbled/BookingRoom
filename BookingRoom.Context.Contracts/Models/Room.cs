namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Номер
    /// </summary>
    public class Room : BaseAuditEntity
    {
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

        public ICollection<Booking> Bookings { get; set; }
    }
}
