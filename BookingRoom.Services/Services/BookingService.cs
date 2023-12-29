using AutoMapper;
using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using BookingRoom.Repositories.Contracts.WriteRepositoriesContracts;
using BookingRoom.Services.Anchors;
using BookingRoom.Services.Contracts.Exceptions;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;
using BookingRoom.Services.Contracts.ServicesContracts;
using BookingRoom.Services.Validator;

namespace BookingRoom.Services.Services
{
    /// <inheritdoc cref="IBookingService"/>
    public class BookingService : IBookingService, IServiceAnchor
    {
        private readonly IBookingWriteRepository bookingWriteRepository;
        private readonly IBookingRedRepository bookingRedRepository;
        private readonly IHotelRedRepository hotelRedRepository;
        private readonly IGuestRedRepository guestRedRepository;
        private readonly IServiceRedRepository serviceRedRepository;
        private readonly IRoomRedRepository roomRedRepository;
        private readonly IStaffRedRepository staffReadRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IServiceValidatorService validatorService;

        public BookingService(IBookingWriteRepository bookingWriteRepository, 
            IBookingRedRepository bookingRedRepository, 
            IHotelRedRepository hotelRedRepository,
            IGuestRedRepository guestRedRepository,
            IRoomRedRepository roomRedRepository,
            IServiceRedRepository serviceRedRepository,
            
            IStaffRedRepository staffReadRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IServiceValidatorService validatorService)
        {
            this.bookingWriteRepository = bookingWriteRepository;
            this.bookingRedRepository = bookingRedRepository;
            this.guestRedRepository = guestRedRepository;
            this.roomRedRepository = roomRedRepository;
            this.serviceRedRepository = serviceRedRepository;
            this.hotelRedRepository = hotelRedRepository;
            this.staffReadRepository = staffReadRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        async Task<BookingModel> IBookingService.AddAsync(BookingRequestModel model, CancellationToken cancellationToken)
        {
            model.Id = Guid.NewGuid();
            await validatorService.ValidateAsync(model, cancellationToken);

            var booking = mapper.Map<Booking>(model);
            bookingWriteRepository.Add(booking);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetBookingModelOnMapping(booking, cancellationToken);
        }

        async Task IBookingService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetBooking = await bookingRedRepository.GetByIdAsync(id, cancellationToken);

            if (targetBooking == null)
            {
                throw new BookingEntityNotFoundException<Booking>(id);
            }

            if (targetBooking.DeletedAt.HasValue)
            {
                throw new BookingInvalidOperationException($"Бронь с идентификатором {id} уже удалена");
            }

            bookingWriteRepository.Delete(targetBooking);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<BookingModel> IBookingService.EditAsync(BookingRequestModel model, CancellationToken cancellationToken)
        {
            await validatorService.ValidateAsync(model, cancellationToken);

            var booking = await bookingRedRepository.GetByIdAsync(model.Id, cancellationToken);

            if (booking == null)
            {
                throw new BookingEntityNotFoundException<Booking>(model.Id);
            }

            booking = mapper.Map<Booking>(model);
            bookingWriteRepository.Update(booking);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return await GetBookingModelOnMapping(booking, cancellationToken);
        }

        async Task<IEnumerable<BookingModel>> IBookingService.GetAllAsync(CancellationToken cancellationToken)
        {
            var bookings = await bookingRedRepository.GetAllAsync(cancellationToken);
            var hotels = await hotelRedRepository
                .GetByIdsAsync(bookings.Select(x => x.HotelId).Distinct(), cancellationToken);

            var guests = await guestRedRepository
                .GetByIdsAsync(bookings.Select(x => x.GuestId).Distinct(), cancellationToken);

            var rooms = await roomRedRepository
                .GetByIdsAsync(bookings.Select(x => x.RoomId).Distinct(), cancellationToken);

            var staffs = await staffReadRepository
                .GetByIdsAsync(bookings.Where(x => x.StaffId.HasValue).Select(x => x.StaffId!.Value).Distinct(), cancellationToken);

            var services = await serviceRedRepository
                .GetByIdsAsync(bookings.Select(x => x.ServiceId).Distinct(), cancellationToken);


            var result = new List<BookingModel>();

            foreach (var booking in bookings)
            {
                if (!hotels.TryGetValue(booking.HotelId, out var hotel) ||
                !guests.TryGetValue(booking.GuestId, out var guest) ||
                !services.TryGetValue(booking.ServiceId, out var service) ||
                !rooms.TryGetValue(booking.RoomId, out var room))
                {
                    continue;
                }
                else
                {
                    var bookingModel = mapper.Map<BookingModel>(booking);

                    bookingModel.Hotel = mapper.Map<HotelModel>(hotel);
                    bookingModel.Guest = mapper.Map<GuestModel>(guest);
                    bookingModel.Room = mapper.Map<RoomModel>(room);
                    bookingModel.Staff = booking.StaffId.HasValue &&
                                              staffs.TryGetValue(booking.StaffId!.Value, out var staff)
                        ? mapper.Map<StaffModel>(staff)
                        : null;

                    bookingModel.Service = mapper.Map<ServiceModel>(service);

                    result.Add(bookingModel);
                }
            }

            return result;
        }

        async Task<BookingModel?> IBookingService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingRedRepository.GetByIdAsync(id, cancellationToken);

            if (item == null)
            {
                throw new BookingEntityNotFoundException<Booking>(id);
            }

            return await GetBookingModelOnMapping(item, cancellationToken);
        }

        async private Task<BookingModel> GetBookingModelOnMapping(Booking booking, CancellationToken cancellationToken)
        {
            var bookingModel = mapper.Map<BookingModel>(booking);
            bookingModel.Hotel = mapper.Map<HotelModel>(await roomRedRepository.GetByIdAsync(booking.HotelId, cancellationToken));
            bookingModel.Guest = mapper.Map<GuestModel>(await guestRedRepository.GetByIdAsync(booking.GuestId, cancellationToken));
            bookingModel.Room = mapper.Map<RoomModel>(await roomRedRepository.GetByIdAsync(booking.RoomId, cancellationToken));
            bookingModel.Staff = mapper.Map<StaffModel>(booking.StaffId.HasValue
                ? await staffReadRepository.GetByIdAsync(booking.StaffId.Value, cancellationToken)
                : null);
            bookingModel.Service = mapper.Map<ServiceModel>(await serviceRedRepository.GetByIdAsync(booking.ServiceId, cancellationToken));
            return bookingModel;
        }
    }
}
