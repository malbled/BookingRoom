namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Постоялец
    /// </summary>
    public class Guest : BaseAuditEntity
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
        /// Паспорт
        /// </summary>
        public string Passport { get; set; } = string.Empty;

        /// <summary>
        /// Адрес регистрации
        /// </summary>
        public string? AddressRegistration { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
