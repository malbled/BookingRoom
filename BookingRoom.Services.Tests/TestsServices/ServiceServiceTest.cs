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
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoom.Services.Tests.TestsServices
{
    public class ServiceServiceTest : BookingRoomContextInMemory
    {
        private readonly IServiceService serviceService;
        private readonly ServiceReadRepository serviceReadRepository;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServiceServiceTest"/>
        /// </summary>
        public ServiceServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceMapper());
            });
            serviceReadRepository = new ServiceReadRepository(Reader);
            serviceService = new ServiceService(new ServiceWriteRepository(WriterContext), serviceReadRepository,
                config.CreateMapper(), UnitOfWork, new ServicesValidatorService(new HotelReadRepository(Reader),
                new GuestReadRepository(Reader), serviceReadRepository, new RoomReadRepository(Reader)));
        }

        /// <summary>
        /// Получение <see cref="Service"/> по идентификатору возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => serviceService.GetByIdAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Service>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Получение <see cref="Service"/> по идентификатору возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Service();
            await Context.Services.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await serviceService.GetByIdAsync(target.Id, CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Title,
                    target.Description
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Service}"/> по идентификаторам возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await serviceService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="Service"/> по идентификаторам возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var target = TestDataGenerator.Service();

            await Context.Services.AddRangeAsync(target,
                TestDataGenerator.Service(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await serviceService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Удаление несуществуюущего <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => serviceService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingEntityNotFoundException<Service>>()
                .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Film"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Service(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Services.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => serviceService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<BookingInvalidOperationException>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Service();
            await Context.Services.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => serviceService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Services.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel();

            //Act
            Func<Task> act = () => serviceService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Services.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Добавление не валидируемого <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task AddShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel(x => x.Title = "T");

            //Act
            Func<Task> act = () => serviceService.AddAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel();

            //Act
            Func<Task> act = () => serviceService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingEntityNotFoundException<Service>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение невалидируемого <see cref="Service"/>
        /// </summary>
        [Fact]
        public async Task EditShouldValidationException()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel(x => x.Title = "T");

            //Act
            Func<Task> act = () => serviceService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<BookingValidationException>();
        }

        /// <summary>
        /// Изменение <see cref="Film"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel();
            var service = TestDataGenerator.Service(x => x.Id = model.Id);
            await Context.Services.AddAsync(service);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => serviceService.EditAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Services.Single(x => x.Id == service.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Title,
                    model.Description,
                });
        }
    }
}
