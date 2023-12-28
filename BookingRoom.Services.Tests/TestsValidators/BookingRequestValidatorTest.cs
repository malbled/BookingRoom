using BookingRoom.Context.Tests;
using BookingRoom.Repositories.ReadRepositories;
using BookingRoom.Services.Validator.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class BookingRequestValidatorTest : BookingRoomContextInMemory
    {
        private readonly CreateBookingRequestValidator validator;

        public BookingRequestValidatorTest()
        {
            validator = new CreateBookingRequestValidator(new HotelReadRepository(Reader), new GuestReadRepository(Reader),
                new ServiceReadRepository(Reader), new RoomReadRepository(Reader));
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.BookingRequestModel(x => { x.DateCheckIn = DateTimeOffset.Now; x.DateCheckout = DateTimeOffset.Now; x.Price = 0; });
            model.GuestId = Guid.NewGuid();
            model.HotelId = Guid.NewGuid();
            model.ServiceId = Guid.NewGuid();
            model.RoomId = Guid.NewGuid();
            model.StaffId = Guid.Empty;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        async public void ValidatorShouldSuccess()
        {
            //Arrange
            var hotel = TestDataGenerator.Hotel();
            var service = TestDataGenerator.Service();
            var room = TestDataGenerator.Room();
            var guest = TestDataGenerator.Guest();

            await Context.Hotels.AddAsync(hotel);
            await Context.Services.AddAsync(service);
            await Context.Rooms.AddAsync(room);
            await Context.Guests.AddAsync(guest);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            var model = TestDataGenerator.BookingRequestModel();
            model.GuestId = guest.Id;
            model.ServiceId = service.Id;
            model.RoomId = room.Id;
            model.HotelId = hotel.Id;
            model.StaffId = Guid.Empty;

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
