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
    public class RoomIntergrationTest : BaseIntegrationTest
    {
        public RoomIntergrationTest(BookingRoomApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var room = mapper.Map<CreateRoomRequest>(TestDataGenerator.RoomModel());

            // Act
            string data = JsonConvert.SerializeObject(room);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Room", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoomResponse>(resultString);

            var roomFirst = await context.Rooms.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            roomFirst.Should()
                .BeEquivalentTo(room);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var room = TestDataGenerator.Room();
            await context.Rooms.AddAsync(room);
            await unitOfWork.SaveChangesAsync();

            var filmRequest = mapper.Map<RoomRequest>(TestDataGenerator.RoomModel(x => x.Id = room.Id));

            // Act
            string data = JsonConvert.SerializeObject(filmRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Room", contextdata);

            var roomFirst = await context.Rooms.FirstAsync(x => x.Id == filmRequest.Id);

            // Assert           
            roomFirst.Should()
                .BeEquivalentTo(filmRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var room = TestDataGenerator.Room();
            await context.Rooms.AddAsync(room);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Room/{room.Id}");

            var roomFirst = await context.Rooms.FirstAsync(x => x.Id == room.Id);

            // Assert
            roomFirst.DeletedAt.Should()
                .NotBeNull();

            roomFirst.Should()
            .BeEquivalentTo(new
            {
                room.Title,
                room.TypeRoom
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var room1 = TestDataGenerator.Room();
            var room2 = TestDataGenerator.Room();

            await context.Rooms.AddRangeAsync(room1, room2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Room/{room1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RoomResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    room1.Id,
                    room1.Title
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var room1 = TestDataGenerator.Room();
            var room2 = TestDataGenerator.Room(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Rooms.AddRangeAsync(room1, room2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Room");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<RoomResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == room1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == room2.Id);
        }
    }
}
