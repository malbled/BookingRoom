namespace BookingRoom.API.Models.CreateRequest
{
    /// <summary>
    /// Модель запроса создания брони
    /// </summary>
    public class CreateBookingRequest
    {
        /// <summary>
        /// Идентификатор отеля
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Идентификатор постояльца
        /// </summary>
        public Guid GuestId { get; set; }

        /// <summary>
        /// Идентификатор номера
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, оформившего бронь
        /// </summary>
        public Guid? StaffId { get; set; }

        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Дата заезда
        /// </summary>
        public DateTimeOffset DateCheckIn { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTimeOffset DateCheckout { get; set; }

        /// <summary>
        /// Итоговая цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
