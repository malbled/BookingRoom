using BookingRoom.API.Enums;

namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания сотрудника
    /// </summary>
    public class CreateStaffRequest
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
        public PostResponse Post { get; set; }
    }
}
