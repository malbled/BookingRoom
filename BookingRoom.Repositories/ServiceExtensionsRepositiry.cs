using BookingRoom.General;
using BookingRoom.Repositories.Anchors;
using Microsoft.Extensions.DependencyInjection;

namespace BookingRoom.Repositories
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensionsRepositiry
    {
        /// <summary>
        /// Регистрация репозиториев
        /// </summary>
        public static void RegistrationRepository(this IServiceCollection service)
        {
            service.RegistrationOnInterface<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
