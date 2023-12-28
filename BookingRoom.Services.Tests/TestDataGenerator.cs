using BookingRoom.Context.Contracts.Models;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;

namespace BookingRoom.Services.Tests
{
    public static class TestDataGenerator
    {
        static public Hotel Hotel(Action<Hotel>? settings = null)
        {
            var result = new Hotel
            {
                Title = $"Аврора",
                Address = $"Приморское 261"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Guest Guest(Action<Guest>? settings = null)
        {
            var result = new Guest
            {
                LastName = $"Малышка",
                FirstName = $"Саня",
                MiddleName = $"Юрьевна",
                Passport = $"12345",
                AddressRegistration = $"Приморское 261"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Room Room(Action<Room>? settings = null)
        {
            var result = new Room
            {
                Title = $"1023",
                TypeRoom = $"люкс",
                Description = $"есть холодильник"
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public Staff Staff(Action<Staff>? settings = null)
        {
            var result = new Staff
            {
                LastName = $"не малышка",
                FirstName = $"не саня",
                MiddleName = $"не юрьевна",
                Post = Context.Contracts.Enums.Post.None
            };
            result.BaseAuditSetParamtrs();
            settings?.Invoke(result);
            return result;
        }

        static public Service Service(Action<Service>? settings = null)
        {
            var result = new Service
            {
                Title = $"услуга",
                Description = $"хорошая"
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
                Price = 101
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        public static HotelModel HotelModel(Action<HotelModel>? settings = null)
        {
            var result = new HotelModel
            {
                Id = Guid.NewGuid(),
                Title = $"Аврора",
                Address = $"Примосркое 261"
            };
            settings?.Invoke(result);
            return result;
        }

        public static GuestModel GuestModel(Action<GuestModel>? settings = null)
        {
            var result = new GuestModel
            {

                Id = Guid.NewGuid(),
                LastName = $"{Guid.NewGuid():N}",
                FirstName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Passport = $"55555",
                AddressRegistration = $"{Guid.NewGuid():N}",
            };

            settings?.Invoke(result);
            return result;
        }

        public static RoomModel RoomModel(Action<RoomModel>? settings = null)
        {
            var result = new RoomModel
            {
                Id = Guid.NewGuid(),
                Title = $"55555",
                TypeRoom = $"{Guid.NewGuid():N}",
                Description = $"{Guid.NewGuid():N}"
            };

            settings?.Invoke(result);
            return result;
        }

        static public StaffModel StaffModel(Action<StaffModel>? settings = null)
        {
            var result = new StaffModel
            {
                Id = Guid.NewGuid(),
                LastName = $"{Guid.NewGuid():N}",
                FirstName = $"{Guid.NewGuid():N}",
                MiddleName = $"{Guid.NewGuid():N}",
                Post = Contracts.Enums.PostModel.None
            };

            settings?.Invoke(result);
            return result;
        }

        static public ServiceModel ServiceModel(Action<ServiceModel>? settings = null)
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


        static public BookingRequestModel BookingRequestModel(Action<BookingRequestModel>? settings = null)
        {
            var result = new BookingRequestModel
            {
                Id = Guid.NewGuid(),
                DateCheckIn = DateTimeOffset.Now.AddDays(1),
                DateCheckout = DateTimeOffset.Now.AddDays(3),
                Price = 101
            };

            settings?.Invoke(result);
            return result;
        }
    }
}
