using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.API.Tests.Infrastructures;
using BookingRoom.Test.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace BookingRoom.API.Tests.IntegrationTests
{
    public class GuestIntergrationTest : BaseIntegrationTest
    {
        public GuestIntergrationTest(BookingRoomApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var guest = mapper.Map<CreateGuestRequest>(TestDataGenerator.GuestModel());

            // Act
            string data = JsonConvert.SerializeObject(guest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Guest", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GuestResponse>(resultString);

            var hotelFirst =  context.Guests.First(x => x.Id == result!.Id);

            // Assert          
            hotelFirst.Should()
                .BeEquivalentTo(guest);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hotel = TestDataGenerator.Guest();
            await context.Guests.AddAsync(hotel);
            await unitOfWork.SaveChangesAsync();

            var hotelRequest = mapper.Map<GuestRequest>(TestDataGenerator.GuestModel(x => x.Id = hotel.Id));

            // Act
            string data = JsonConvert.SerializeObject(hotelRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Guest", contextdata);

            var hotelFirst =  context.Guests.First(x => x.Id == hotelRequest.Id);

            // Assert           
            hotelFirst.Should()
                .BeEquivalentTo(hotelRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hotel1 = TestDataGenerator.Guest();
            var hotel2 = TestDataGenerator.Guest();

            await context.Guests.AddRangeAsync(hotel1, hotel2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Guest/{hotel1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GuestResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    hotel1.Id,
                    //hotel1.LastName,
                    //hotel1.FirstName,
                    //hotel1.MiddleName,
                    hotel1.Passport,
                    hotel1.AddressRegistration
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hotel1 = TestDataGenerator.Guest();
            var hotel2 = TestDataGenerator.Guest(x => x.DeletedAt = DateTimeOffset.UtcNow);

            await context.Guests.AddRangeAsync(hotel1, hotel2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Guest");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<GuestResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == hotel1.Id)
                .And
                .NotContain(x => x.Id == hotel2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var hotel = TestDataGenerator.Guest();
            await context.Guests.AddAsync(hotel);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Guest/{hotel.Id}");

            var hotelFirst =  context.Guests.First(x => x.Id == hotel.Id);

            // Assert
            hotelFirst.DeletedAt.Should()
                .NotBeNull();

            hotelFirst.Should()
                .BeEquivalentTo(new
                {
                    hotel.Passport,
                    hotel.AddressRegistration
                });
        }
    }
}
