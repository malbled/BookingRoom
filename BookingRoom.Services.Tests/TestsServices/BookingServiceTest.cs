using AutoMapper;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Context.Tests;
using BookingRoom.Repositories.ReadRepositories;
using BookingRoom.Repositories.WriteRepositories;
using BookingRoom.Services.AutoMappers;
using BookingRoom.Services.Contracts.Exceptions;
using BookingRoom.Services.Contracts.ServicesContracts;
using BookingRoom.Services.Services;
using BookingRoom.Services.Validator;
using BookingRoom.Test.Extensions;
using FluentAssertions;
using Xunit;

namespace BookingRoom.Services.Tests.TestsServices
{
    public class BookingServiceTest : BookingRoomContextInMemory
    {
        private readonly IBookingService bookingService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="TicketServiceTest"/>
        /// </summary>
        public BookingServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            bookingService = new BookingService(new BookingWriteRepository(WriterContext), new BookingReadRepository(Reader),
                new HotelReadRepository(Reader), new GuestReadRepository(Reader), new ServiceReadRepository(Reader),
                new RoomReadRepository(Reader),
                new StaffReadRepository(Reader), config.CreateMapper(), UnitOfWork,
                new ServicesValidatorService(new HotelReadRepository(Reader),
                new GuestReadRepository(Reader), new ServiceReadRepository(Reader), new RoomReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Booking"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => bookingService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Booking>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Booking"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Booking();
            await Context.Bookings.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.DateCheckIn,
                    target.DateCheckout,
                    target.Price
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Booking}"/> по идентификаторам возвращает пустйю коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await bookingService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Booking}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Booking();
            await Context.Bookings.AddRangeAsync(target,
                TestDataGenerator.Booking(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await bookingService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(0);
        }

        /// <summary>
        /// Удаление не существуюущего <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentBookingReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => bookingService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Booking>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedBookingReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Booking(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Bookings.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => bookingService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Booking();
            await Context.Bookings.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => bookingService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Bookings.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var hotel = TestDataGenerator.Hotel();
            var service = TestDataGenerator.Service();
            var room = TestDataGenerator.Room();
            var guest = TestDataGenerator.Guest();

            await Context.Hotels.AddAsync(hotel);
            await Context.Guests.AddAsync(guest);
            await Context.Rooms.AddAsync(room);
            await Context.Services.AddAsync(service);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.BookingRequestModel();
            model.HotelId = hotel.Id;
            model.GuestId = guest.Id;
            model.RoomId = room.Id;
            model.ServiceId = service.Id;

            //Act
            Func<Task> act = () => bookingService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Bookings.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.BookingRequestModel();

            //Act
            Func<Task> act = () => bookingService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var hotel = TestDataGenerator.Hotel();
            var service = TestDataGenerator.Service();
            var room = TestDataGenerator.Room();
            var guest = TestDataGenerator.Guest();

            await Context.Hotels.AddAsync(hotel);
            await Context.Guests.AddAsync(guest);
            await Context.Rooms.AddAsync(room);
            await Context.Services.AddAsync(service);

            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.BookingRequestModel();
            model.HotelId = hotel.Id;
            model.GuestId = guest.Id;
            model.RoomId = room.Id;
            model.ServiceId = service.Id;

            //Act
            Func<Task> act = () => bookingService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingEntityNotFoundException<Booking>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Booking"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.BookingRequestModel();

            //Act
            Func<Task> act = () => bookingService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Ticket"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var hotel = TestDataGenerator.Hotel();
            var service = TestDataGenerator.Service();
            var room = TestDataGenerator.Room();
            var guest = TestDataGenerator.Guest();

            await Context.Hotels.AddAsync(hotel);
            await Context.Guests.AddAsync(guest);
            await Context.Rooms.AddAsync(room);
            await Context.Services.AddAsync(service);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var booking = TestDataGenerator.Booking();
            booking.HotelId = hotel.Id;
            booking.GuestId = guest.Id;
            booking.RoomId = room.Id;
            booking.ServiceId = service.Id;

            var model = TestDataGenerator.BookingRequestModel();
                model.Id = booking.Id;

            model.HotelId = hotel.Id;
            model.GuestId = guest.Id;
            model.RoomId = room.Id;
            model.ServiceId = service.Id;

            await Context.Bookings.AddAsync(booking);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => bookingService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Bookings.Single(x => x.Id == booking.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.DateCheckIn,
                    model.DateCheckout,
                    model.Price
                    
                });
        }
    }
}
