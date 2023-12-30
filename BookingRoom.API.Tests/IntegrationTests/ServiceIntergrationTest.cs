using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.API.Tests.Infrastructures;
using BookingRoom.Test.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoom.API.Tests.IntegrationTests
{
    public class ServiceIntergrationTest : BaseIntegrationTest
    {
        public ServiceIntergrationTest(BookingRoomApiFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var service = mapper.Map<CreateServiceRequest>(TestDataGenerator.ServiceModel());

            // Act
            string data = JsonConvert.SerializeObject(service);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Service", contextdata);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse>(resultString);

            var serviceFirst = await context.Services.FirstAsync(x => x.Id == result!.Id);

            // Assert          
            serviceFirst.Should()
                .BeEquivalentTo(service);
        }

        [Fact]
        public async Task EditShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var service = TestDataGenerator.Service();
            await context.Services.AddAsync(service);
            await unitOfWork.SaveChangesAsync();

            var serviceRequest = mapper.Map<ServiceRequest>(TestDataGenerator.ServiceModel(x => x.Id = service.Id));

            // Act
            string data = JsonConvert.SerializeObject(serviceRequest);
            var contextdata = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync("/Service", contextdata);

            var serviceFirst = await context.Services.FirstAsync(x => x.Id == serviceRequest.Id);

            // Assert           
            serviceFirst.Should()
                .BeEquivalentTo(serviceRequest);
        }

        [Fact]
        public async Task DeleteShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var service = TestDataGenerator.Service();
            await context.Services.AddAsync(service);
            await unitOfWork.SaveChangesAsync();

            // Act
            await client.DeleteAsync($"/Service/{service.Id}");

            var serviceFirst = await context.Services.FirstAsync(x => x.Id == service.Id);

            // Assert
            serviceFirst.DeletedAt.Should()
                .NotBeNull();

            serviceFirst.Should()
            .BeEquivalentTo(new
            {
                service.Title,
                service.Description
            });
        }

        [Fact]
        public async Task GetShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var service1 = TestDataGenerator.Service();

            await context.Services.AddRangeAsync(service1);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync($"/Service/{service1.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    service1.Id,
                    service1.Title
                });
        }

        [Fact]
        public async Task GetAllShouldWork()
        {
            // Arrange
            var client = factory.CreateClient();
            var service1 = TestDataGenerator.Service();
            var service2 = TestDataGenerator.Service(x => x.DeletedAt = DateTimeOffset.Now);

            await context.Services.AddRangeAsync(service1, service2);
            await unitOfWork.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/Service");

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<ServiceResponse>>(resultString);

            result.Should()
                .NotBeNull()
                .And
                .Contain(x => x.Id == service1.Id);

            result.Should()
                .NotBeNull()
                .And
                .NotContain(x => x.Id == service2.Id);
        }
    }
}
