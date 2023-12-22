namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания отеля
    /// </summary>
    public class CreateHotelRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
