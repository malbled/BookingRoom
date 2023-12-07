namespace BookingRoom.Services.Contracts.Models
{
    /// <summary>
    /// Модель услуги
    /// </summary>
    public class ServiceModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название услуги
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; set; }
    }
}
