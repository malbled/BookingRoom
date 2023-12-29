namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания постояльца
    /// </summary>
    public class CreateGuestRequest
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
        public string AddressRegistration { get; set; } = string.Empty;

    }
}
