using BookingRoom.Common.Entity;

namespace BookingRoom.API.Extensions
{
    /// <summary>
    /// Реализация <see cref="IDateTimeProvider"/>
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTimeOffset IDateTimeProvider.UtcNow => DateTimeOffset.UtcNow;
    }
}
