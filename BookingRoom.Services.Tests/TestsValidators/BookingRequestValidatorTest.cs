using BookingRoom.Context.Contracts.Enums;
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
            var model = TestDataGenerator.BookingRequestModel(x => { x.DateCheckIn = DateTimeOffset.Now; x.DateCheckout = DateTimeOffset.Now; x.Price = 100; });
            
            model.HotelId = Guid.NewGuid();
            model.GuestId = Guid.NewGuid();
            model.RoomId = Guid.NewGuid();
            model.StaffId = Guid.Empty;
            model.ServiceId = Guid.NewGuid();

            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorShouldSuccess()
        {
            //Arrange
            var hotel = TestDataGenerator.Hotel();
            var guest = TestDataGenerator.Guest();
            var room = TestDataGenerator.Room();
            var service = TestDataGenerator.Service();

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
            model.DateCheckIn = DateTime.Now;
            model.DateCheckout = DateTime.Now;
            model.Price = 101;


            // Act
            var result = await validator.TestValidateAsync(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
