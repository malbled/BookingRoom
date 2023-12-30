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
    public class StaffIntergrationTest : BaseIntegrationTest
    {
        public StaffIntergrationTest(BookingRoomApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var staff = mapper.Map<CreateStaffRequest>(TestDataGenerator.StaffModel());

            // Act
            string data = JsonConvert.SerializeObject(staff);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Staff", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<StaffResponse>(resultString);

            var staffFirst = await context.Staffs.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            staffFirst.Should()
                .BeEquivalentTo(staff);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var staff = TestDataGenerator.Staff();
            await context.Staffs.AddAsync(staff);
            await unitOfWork.SaveChangesAsync();

            var staffRequest = mapper.Map<StaffRequest>(TestDataGenerator.StaffModel(x => x.Id = staff.Id));

            // Act
            string data = JsonConvert.SerializeObject(staffRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Staff", contextdata);

            var staffFirst = await context.Staffs.FirstAsync(x => x.Id == staffRequest.Id);

            // Assert           
            staffFirst.Should()
                .BeEquivalentTo(staffRequest);
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var staff1 = TestDataGenerator.Staff();
            var staff2 = TestDataGenerator.Staff();

            await context.Staffs.AddRangeAsync(staff1, staff2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Staff/{staff1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<StaffResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    staff1.Id,
                    staff1.Post
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var staff1 = TestDataGenerator.Staff();
            var staff2 = TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Staffs.AddRangeAsync(staff1, staff2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Staff");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<StaffResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == staff1.Id)
                .And
                .NotContain(x => x.Id == staff2.Id);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var staff = TestDataGenerator.Staff();
            await context.Staffs.AddAsync(staff);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Staff/{staff.Id}");

            var staffFirst = await context.Staffs.FirstAsync(x => x.Id == staff.Id);

            // Assert
            staffFirst.DeletedAt.Should()
                .NotBeNull();

            staffFirst.Should()
                .BeEquivalentTo(new
                {
                    staff.FirstName,
                    staff.Post
                });
        }
    }
}
