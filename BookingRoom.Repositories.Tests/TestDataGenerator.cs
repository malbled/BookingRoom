using BookingRoom.Context.Contracts.Models;

namespace BookingRoom.Repositories.Tests
{
    internal static class TestDataGenerator
    {
        static internal Hotel Hotel(Action<Hotel>? settings = null)
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

        static internal Service Service(Action<Service>? settings = null)
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

        static internal Room Room(Action<Room>? settings = null)
        {
            var result = new Room
            {
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                TypeRoom = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Guest Guest(Action<Guest>? settings = null)
        {
            var result = new Guest
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Passport = $"{Guid.NewGuid():N}",
                AddressRegistration = $"{Guid.NewGuid():N}"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal Staff Staff(Action<Staff>? settings = null)
        {
            var result = new Staff
            {
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Post = Context.Contracts.Enums.Post.Administrator
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }

        static internal Booking Booking(Action<Booking>? settings = null)
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
