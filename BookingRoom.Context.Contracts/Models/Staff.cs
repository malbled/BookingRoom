using BookingRoom.Context.Contracts.Enums;

namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Работник
    /// </summary>
    public class Staff : BaseAuditEntity
    {
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; } = string.Empty;

        /// <summary>
        /// Должность
        /// </summary>
        public Post Post { get; set; } = Post.None;

        public ICollection<Booking>? Bookings { get; set; }
    }
}
