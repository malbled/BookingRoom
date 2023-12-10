using BookingRoom.General;

namespace BookingRoom.API.Exceptions
{
    /// <summary>
    /// Информация об ошибках валидации работы API
    /// </summary>
    public class APIValidationExceptionDetail
    {
        /// <summary>
        /// Ошибки валидации
        /// </summary>
        public IEnumerable<InvalidateItemModel> Errors { get; set; } = Array.Empty<InvalidateItemModel>();
    }
}
