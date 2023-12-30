using AutoMapper;
using BookingRoom.API.AutoMappers;
using BookingRoom.API.Tests.Infrastructures;
using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts;
using BookingRoom.Services.AutoMappers;
using Xunit;

namespace BookingRoom.API.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(BookingRoomApiTestCollection))]
    public class BaseIntegrationTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly IBookingRoomContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrationTest(BookingRoomApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new APIMappers(), new ServiceMapper() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}
