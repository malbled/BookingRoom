using BookingRoom.Services.Contracts.Enums;

namespace BookingRoom.Services.Contracts.Models
{
    /// <summary>
    /// Модель сотрудника
    /// </summary>
    public class StaffModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

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
        public PostModel Post { get; set; }
    }
}
