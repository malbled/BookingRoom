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
    public class GuestServiceTest : BookingRoomContextInMemory
    {
        private readonly IGuestService guestService;
        private readonly GuestReadRepository guestReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GuestServiceTest"/>
        /// </summary>
        public GuestServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });

            guestReadRepository = new GuestReadRepository(Reader);

            guestService = new GuestService(
                new GuestWriteRepository(WriterContext), 
                guestReadRepository,
                UnitOfWork, 
                config.CreateMapper(), 
                
                new ServicesValidatorService(
                    new HotelReadRepository(Reader),
                    guestReadRepository, 
                    new ServiceReadRepository(Reader), 
                    new RoomReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Guest"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => guestService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Guest>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Guest"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Guest();
            await Context.Guests.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await guestService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.LastName,
                    target.FirstName,
                    target.MiddleName,
                    target.Passport,
                    target.AddressRegistration
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Guest}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await guestService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Guest}"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Guest();

            await Context.Guests.AddRangeAsync(target,
                TestDataGenerator.Guest(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await guestService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentGuestReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => guestService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Guest>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedGuestReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Guest(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Guests.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => guestService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Guest();
            await Context.Guests.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => guestService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Guests.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel();

            //Act
            Func<Task> act = () => guestService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Guests.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление невалидируемого <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel(x => x.FirstName = "T");

            //Act
            Func<Task> act = () => guestService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel();

            //Act
            Func<Task> act = () => guestService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingEntityNotFoundException<Guest>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel(x => x.FirstName = "T");

            //Act
            Func<Task> act = () => guestService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Guest"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel();
            var guest = TestDataGenerator.Guest(x => x.Id = model.Id);
            await Context.Guests.AddAsync(guest);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => guestService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Guests.Single(x => x.Id == guest.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.LastName,
                    model.FirstName,
                    model.MiddleName,
                    model.Passport,
                    model.AddressRegistration
                });
        }
    }
}
