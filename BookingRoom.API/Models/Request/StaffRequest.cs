using BookingRoom.API.Models.CreateRequest;

namespace BookingRoom.API.Models.Request
{
    /// <summary>
    /// Модель запроса создания сотрудника
    /// </summary>
    public class StaffRequest : CreateStaffRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
