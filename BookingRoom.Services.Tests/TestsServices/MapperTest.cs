using AutoMapper;
using BookingRoom.Services.AutoMappers;
using Xunit;

namespace BookingRoom.Services.Tests.TestsServices
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<ServiceMapper>());
            configuration.AssertConfigurationIsValid();
        }
    }
}
