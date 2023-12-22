using BookingRoom.API.Models.CreateRequest;

namespace BookingRoom.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания услуги
    /// </summary>
    public class ServiceRequest : CreateServiceRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
