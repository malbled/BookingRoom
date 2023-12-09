namespace BookingRoom.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Модель запроса создания брони
    /// </summary>
    public class BookingRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор номера
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Идентификатор отеля
        /// </summary>
        public Guid HotelId { get; set; }

        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public Guid ServiceId { get; set; }

        /// <summary>
        /// Идентификатор постояльца
        /// </summary>
        public Guid GuestId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, создавшего бронь 
        /// </summary>
        public Guid? StaffId { get; set; }

        /// <summary>
        /// Дата и время заезда
        /// </summary>
        public DateTimeOffset DateCheckIn { get; set; }

        /// <summary>
        /// Дата и время выезда
        /// </summary>
        public DateTimeOffset DateCheckout { get; set; }

        /// <summary>
        /// Итоговая Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
