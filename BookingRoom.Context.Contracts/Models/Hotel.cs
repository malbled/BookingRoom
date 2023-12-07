namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Отель
    /// </summary>
    public class Hotel : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; }
    }
}
