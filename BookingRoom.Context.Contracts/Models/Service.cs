namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Услуга
    /// </summary>
    public class Service : BaseAuditEntity
    {
        /// <summary>
        /// Название услуги
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
