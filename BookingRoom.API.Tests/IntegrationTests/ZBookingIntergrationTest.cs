using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.API.Tests.Infrastructures;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Test.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Sockets;
using System;
using System.Text;
using Xunit;

namespace BookingRoom.API.Tests.IntegrationTests
{
    public class ZBookingIntergrationTest : BaseIntegrationTest
    {
        private readonly Hotel hotel;
        private readonly Guest guest;
        private readonly Room room;
        private readonly Service service;

        public ZBookingIntergrationTest(BookingRoomApiFixture fixture) : base(fixture)
        {
            hotel = TestDataGenerator.Hotel();
            guest = TestDataGenerator.Guest();
            room = TestDataGenerator.Room();
            service = TestDataGenerator.Service();

            context.Hotels.Add(hotel);
            context.Guests.Add(guest);
            context.Rooms.Add(room);
            context.Services.Add(service);
            unitOfWork.SaveChangesAsync();
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var clientFactory = factory.CreateClient();

            var booking = mapper.Map<CreateBookingRequest>(TestDataGenerator.BookingRequestModel());
            booking.HotelId = hotel.Id;
            booking.GuestId = guest.Id;
            booking.RoomId = room.Id;
            booking.ServiceId = service.Id;

            // Act
            string data = JsonConvert.SerializeObject(booking);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await clientFactory.PostAsync("/Booking", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookingResponse>(resultString);

            var bookingFirst =  context.Bookings.First(x => x.Id == result!.Id);

            // Assert          
            bookingFirst.Should()
                .BeEquivalentTo(booking);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var booking = TestDataGenerator.Booking();

            SetDependenciesOrBooking(booking);
            await context.Bookings.AddAsync(booking);
            await unitOfWork.SaveChangesAsync();

            var bookingRequest = mapper.Map<BookingRequest>(TestDataGenerator.BookingRequestModel(x => x.Id = booking.Id));
            SetDependenciesOrBookingRequestModelWithBooking(booking, bookingRequest);

            // Act
            string data = JsonConvert.SerializeObject(bookingRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Booking", contextdata);

            var bookingFirst =  context.Bookings.FirstOrDefault(x => x.Id == bookingRequest.Id);

            // Assert           
            bookingFirst.Should()
                .BeEquivalentTo(bookingRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var booking1 = TestDataGenerator.Booking();
            var booking2 = TestDataGenerator.Booking();

            SetDependenciesOrBooking(booking1);
            SetDependenciesOrBooking(booking2);

            await context.Bookings.AddRangeAsync(booking1, booking2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Booking/{booking1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BookingResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    booking1.Id,
                    booking1.DateCheckIn,
                    booking1.DateCheckout,
                    booking1.Price
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var booking1 = TestDataGenerator.Booking();
            var booking2 = TestDataGenerator.Booking(x => x.DeletedAt = DateTimeOffset.Now);

            SetDependenciesOrBooking(booking1);
            SetDependenciesOrBooking(booking2);

            await context.Bookings.AddRangeAsync(booking1, booking2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Booking");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<BookingResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == booking1.Id)
                .And
                .NotContain(x => x.Id == booking2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var booking = TestDataGenerator.Booking();

            SetDependenciesOrBooking(booking);
            await context.Bookings.AddAsync(booking);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Booking/{booking.Id}");

            var bookingFirst =  context.Bookings.FirstOrDefault(x => x.Id == booking.Id);

            // Assert
            bookingFirst.DeletedAt.Should()
                .NotBeNull();

            bookingFirst.Should()
                .BeEquivalentTo(new
                {
                    booking.HotelId,
                    booking.GuestId,
                    booking.RoomId,
                    booking.ServiceId,
                    booking.DateCheckIn,
                    booking.DateCheckout,
                    booking.Price
                });
        }

        private void SetDependenciesOrBooking(Booking booking)
        {
            booking.HotelId = hotel.Id;
            booking.GuestId = guest.Id;
            booking.RoomId = room.Id;
            booking.ServiceId = service.Id;
        }

        private void SetDependenciesOrBookingRequestModelWithBooking(Booking booking, BookingRequest bookingRequest)
        {
            bookingRequest.HotelId = booking.HotelId;
            bookingRequest.GuestId = booking.GuestId;
            bookingRequest.RoomId = booking.RoomId;
            bookingRequest.ServiceId = booking.ServiceId;
        }
    }
}
