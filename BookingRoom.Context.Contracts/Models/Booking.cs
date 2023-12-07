namespace BookingRoom.Context.Contracts.Models
{
    /// <summary>
    /// Бронь номера
    /// </summary>
    public class Booking : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор Отеля
        /// </summary>
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }

        /// <summary>
        /// Идентификатор Постояльца
        /// </summary>
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }

        /// <summary>
        /// Идентификатор Номера
        /// </summary>
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        /// <summary>
        /// Идентификатор Сотрудника
        /// </summary>
        public Guid? StaffId { get; set; }
        public Staff? Staff { get; set; }

        /// <summary>
        /// Идентификатор Услуги
        /// </summary>
        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

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
