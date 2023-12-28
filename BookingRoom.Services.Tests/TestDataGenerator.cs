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

                Title = $"55555",
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
                Passport = $"55555",
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
                Post = Context.Contracts.Enums.Post.None
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
                Price = 100
            };
            result.BaseAuditSetParamtrs();

            settings?.Invoke(result);
            return result;
        }

        static public HotelModel HotelModel(Action<HotelModel>? settings = null)
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

        static public RoomModel RoomModel(Action<RoomModel>? settings = null)
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

        static public GuestModel GuestModel(Action<GuestModel>? settings = null)
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
        

        static public BookingRequestModel BookingRequestModel(Action<BookingRequestModel>? settings = null)
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
