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
    public class RoomServiceTest : BookingRoomContextInMemory
    {
        private readonly IRoomService roomService;
        private readonly RoomReadRepository roomReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="RoomServiceTest"/>
        /// </summary>
        public RoomServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            roomReadRepository = new RoomReadRepository(Reader);

            roomService = new RoomService(
                roomReadRepository,
                config.CreateMapper(),
                new RoomWriteRepository(WriterContext), 
                UnitOfWork, 
                
                
                new ServicesValidatorService(
                    new HotelReadRepository(Reader),
                    new GuestReadRepository(Reader), 
                    new ServiceReadRepository(Reader), 
                    roomReadRepository));
        }

        /// <summary>
        /// Получение <see cref="Room"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => roomService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Room>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Room"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Room();
            await Context.Rooms.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await roomService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.TypeRoom,
                    target.Description
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Room}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await roomService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Room"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Room();

            await Context.Rooms.AddRangeAsync(target,
                TestDataGenerator.Room(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await roomService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentRoomReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => roomService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Room>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedRoomReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Room(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Rooms.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => roomService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Room>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Room();
            await Context.Rooms.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => roomService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Rooms.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model =  TestDataGenerator.RoomModel();

            //Act
            Func<Task> act = () => roomService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Rooms.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.RoomModel(x => x.TypeRoom = "T");

            //Act
            Func<Task> act = () => roomService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.RoomModel();

            //Act
            Func<Task> act = () => roomService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingEntityNotFoundException<Room>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.RoomModel(x => x.TypeRoom = "T");

            //Act
            Func<Task> act = () => roomService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Room"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.RoomModel();
            var room = TestDataGenerator.Room(x => x.Id = model.Id);
            await Context.Rooms.AddAsync(room);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => roomService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Rooms.Single(x => x.Id == room.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Title,
                    model.TypeRoom,
                    model.Description
                });
        }
    }
}
