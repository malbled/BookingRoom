namespace BookingRoom.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности постояльца
    /// </summary>
    public class GuestResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Паспорт
        /// </summary>
        public string Passport { get; set; } = string.Empty;

        /// <summary>
        /// Адрес регистрации
        /// </summary>
        public string AddressRegistration { get; set; } = string.Empty;
    }
}
