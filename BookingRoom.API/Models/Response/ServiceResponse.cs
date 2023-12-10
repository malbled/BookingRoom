namespace BookingRoom.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности услуги
    /// </summary>
    public class ServiceResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}
