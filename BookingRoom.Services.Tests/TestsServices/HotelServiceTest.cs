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
using FluentAssertions;
using Xunit;

namespace BookingRoom.Services.Tests.TestsServices
{
    public class HotelServiceTest : BookingRoomContextInMemory
    {
        private readonly IHotelService hotelService;
        private readonly HotelReadRepository hotelRead;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="HotelServiceTest"/>
        /// </summary>
        public HotelServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            hotelRead = new HotelReadRepository(Reader);

            hotelService = new HotelService(
                hotelRead, 
                config.CreateMapper(),
                new HotelWriteRepository(WriterContext), 
                UnitOfWork,

                new ServicesValidatorService(hotelRead, 
                new GuestReadRepository(Reader), 
                new ServiceReadRepository(Reader),
                new RoomReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Hotel"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => hotelService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Hotel>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Hotel"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Hotel();
            await Context.Hotels.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await hotelService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Address
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Hotel}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await hotelService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Hotel}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Hotel();

            await Context.Hotels.AddRangeAsync(target,
                TestDataGenerator.Hotel(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await hotelService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldNotFoundException()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => hotelService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Hotel>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldInvalidException()
        {
            //Arrange
            var model = TestDataGenerator.Hotel(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Hotels.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => hotelService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Hotel();
            await Context.Hotels.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => hotelService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Hotels.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel();

            //Act
            Func<Task> act = () => hotelService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Hotels.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel(x => x.Address = "T");

            //Act
            Func<Task> act = () => hotelService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel();

            //Act
            Func<Task> act = () => hotelService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingEntityNotFoundException<Hotel>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel(x => x.Address = "T");

            //Act
            Func<Task> act = () => hotelService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Hotel"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel();
            var hotel = TestDataGenerator.Hotel(x => x.Id = model.Id);
            await Context.Hotels.AddAsync(hotel);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => hotelService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Hotels.Single(x => x.Id == hotel.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Address,
                    model.Title
                });
        }
    }
}
