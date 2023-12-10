namespace BookingRoom.API.Exceptions
{
    /// <summary>
    /// Информация об ошибке работы API
    /// </summary>
    public class APIExceptionDetail
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
