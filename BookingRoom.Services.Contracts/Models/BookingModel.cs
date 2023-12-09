namespace BookingRoom.Services.Contracts.Models
{
    /// <summary>
    /// Модель брони
    /// </summary>
    public class BookingModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Индетификатор Отеля
        /// </summary>
        public HotelModel Hotel { get; set; }

        /// <summary>
        /// Индетификатор Постояльца
        /// </summary>
        public GuestModel Guest { get; set; }

        /// <summary>
        /// Идентификатор Номера
        /// </summary>
        public RoomModel Room { get; set; }

        /// <summary>
        /// Индетификатор Сотрудника
        /// </summary>
        public StaffModel? Staff { get; set; }

        /// <summary>
        /// Идентификатор Услуги
        /// </summary>
        public ServiceModel Service { get; set; }

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
