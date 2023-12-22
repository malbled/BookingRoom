namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания услуги
    /// </summary>
    public class CreateServiceRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
