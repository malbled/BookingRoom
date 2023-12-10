namespace BookingRoom.API.Models.Response
{
    /// <summary>
    /// Модель ответа сущности брони
    /// </summary>
    public class BookingResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сущность отеля
        /// </summary>
        public HotelResponse? Hotel { get; set; }

        /// <summary>
        /// Сущность постояльца
        /// </summary>
        public GuestResponse? Guest { get; set; }

        /// <summary>
        /// Сущность услуги
        /// </summary>
        public ServiceResponse? Service { get; set; }

        /// <summary>
        /// Сущность номера
        /// </summary>
        public RoomResponse? Room { get; set; }

        /// <summary>
        /// Сущность сотрудника
        /// </summary>
        public StaffResponse? Staff { get; set; }

        /// <summary>
        /// Итоговая цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Дата заезда
        /// </summary>
        public string DateCheckIn { get; set; } = string.Empty;

        /// <summary>
        /// Дата выезда
        /// </summary>
        public string DateCheckout { get; set; } = string.Empty;
    }
}
