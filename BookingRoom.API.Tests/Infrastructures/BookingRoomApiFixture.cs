using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context;
using BookingRoom.Context.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BookingRoom.API.Tests.Infrastructures
{
    public class BookingRoomApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private BookingRoomContext? bookingRoomContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="BookingRoomApiFixture"/>
        /// </summary>
        public BookingRoomApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => BookingRoomContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await BookingRoomContext.Database.EnsureDeletedAsync();
            await BookingRoomContext.Database.CloseConnectionAsync();
            await BookingRoomContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IBookingRoomContext Context => BookingRoomContext;

        public IUnitOfWork UnitOfWork => BookingRoomContext;

        internal BookingRoomContext BookingRoomContext
        {
            get
            {
                if (bookingRoomContext != null)
                {
                    return bookingRoomContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                bookingRoomContext = scope.ServiceProvider.GetRequiredService<BookingRoomContext>();
                return bookingRoomContext;
            }
        }
    }
}
