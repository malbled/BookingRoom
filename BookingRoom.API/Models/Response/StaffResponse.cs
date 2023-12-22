using BookingRoom.API.Enums;

namespace BookingRoom.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности персонала
    /// </summary>
    public class StaffResponse
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
        /// Должность
        /// </summary>
        public PostResponse Post { get; set; }
    }
}
