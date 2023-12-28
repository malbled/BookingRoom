using BookingRoom.Context.Contracts.Models;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;

namespace BookingRoom.Services.Tests
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
                Price = 100
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static internal HotelModel HotelModel(Action<HotelModel>? settings = null)
        {
            var result = new HotelModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Address = $"{Guid.NewGuid():N}"
            };
            settings?.Invoke(result);
            return result;
        }

        static internal ServiceModel ServiceModel(Action<ServiceModel>? settings = null)
        {
            var result = new ServiceModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal RoomModel RoomModel(Action<RoomModel>? settings = null)
        {
            var result = new RoomModel
            {
                Id = Guid.NewGuid(),
                Title = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}",
                TypeRoom = $"{Guid.NewGuid():N}"
            };

            settings?.Invoke(result);
            return result;
        }

        static internal GuestModel GuestModel(Action<GuestModel>? settings = null)
        {
            var result = new GuestModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Passport = $"{Guid.NewGuid():N}",
                AddressRegistration = $"{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }

        static internal StaffModel StaffModel(Action<StaffModel>? settings = null)
        {
            var result = new StaffModel
            {
                Id = Guid.NewGuid(),
                FirstName = $"{Guid.NewGuid():N}",
                LastName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Post = Contracts.Enums.PostModel.None
            };

            settings?.Invoke(result);
            return result;
        }

        static internal BookingRequestModel BookingRequestModel(Action<BookingRequestModel>? settings = null)
        {
            var result = new BookingRequestModel
            {
                Id = Guid.NewGuid(),
                DateCheckIn = DateTimeOffset.Now.AddDays(1),
                DateCheckout = DateTimeOffset.Now.AddDays(3),
                Price = 20000
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
