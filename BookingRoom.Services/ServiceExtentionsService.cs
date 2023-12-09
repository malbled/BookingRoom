using BookingRoom.General;
using BookingRoom.Services.Anchors;
using BookingRoom.Services.Validator;
using Microsoft.Extensions.DependencyInjection;

namespace BookingRoom.Services
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtentionsService
    {
        /// <summary>
        /// Регистрация всех сервисов и валидатора
        /// </summary>
        public static void RegistrationService(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IServiceAnchor>(ServiceLifetime.Scoped);
            service.AddTransient<IServiceValidatorService, ServicesValidatorService>();
        }
    }
}
