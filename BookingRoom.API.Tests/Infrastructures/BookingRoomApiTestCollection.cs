using Xunit;

namespace BookingRoom.API.Tests.Infrastructures
{
    [CollectionDefinition(nameof(BookingRoomApiTestCollection))]
    public class BookingRoomApiTestCollection
        : ICollectionFixture<BookingRoomApiFixture>
    {
    }
}
