using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Tests
{
    public static class TestDataGenerator
    {
        static public Hotel Hotel(Action<Hotel>? settings = null)
        {
            var result = new Hotel
            {
                Title = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Service Service(Action<Service>? settings = null)
        {
            var result = new Service
            {
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Room Room(Action<Room>? settings = null)
        {
            var result = new Room
            {
                Title = $"{Guid.NewGuid():N}",
                TypeRoom = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Guest Guest(Action<Guest>? settings = null)
        {
            var result = new Guest
            {
                LastName = $"{Guid.NewGuid():N}",
                FirstName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Passport = $"{Guid.NewGuid():N}",
                AddressRegistration = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Staff Staff(Action<Staff>? settings = null)
        {
            var result = new Staff
            {
                LastName = $"{Guid.NewGuid():N}",
                FirstName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Post = Context.Contracts.Enums.Post.Administrator
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }

        static public Booking Booking(Action<Booking>? settings = null)
        {
            var result = new Booking
            {
                DateCheckIn = DateTimeOffset.Now,
                DateCheckout = DateTimeOffset.Now,
                Price = 20000
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }
    }
}
