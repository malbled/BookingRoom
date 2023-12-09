using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts;

namespace BookingRoom.Context
{
    /// <summary>
    /// Методы расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsContext
    {
        /// <summary>
        /// Регистрирует все что связано с контекстом
        /// </summary>
        /// <param name="service"></param>
        public static void RegistrationContext(this IServiceCollection service)
        {
            service.TryAddScoped<IBookingRoomContext>(provider => provider.GetRequiredService<BookingRoomContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<BookingRoomContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<BookingRoomContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<BookingRoomContext>());
        }
    }
}
