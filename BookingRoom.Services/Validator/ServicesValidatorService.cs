using BookingRoom.General;
using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using BookingRoom.Services.Validator.Validators;
using FluentValidation;
using BookingRoom.Services.Contracts.Exceptions;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;


namespace BookingRoom.Services.Validator
{
    public sealed class ServicesValidatorService : IServiceValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();
        public ServicesValidatorService(IHotelRedRepository hotelRedRepository, IGuestRedRepository guestRedRepository,
            IServiceRedRepository serviceRedRepository, IRoomRedRepository roomRedRepository)
        {
            validators.Add(typeof(HotelModel), new CreateHotelRequestValidator());
            validators.Add(typeof(GuestModel), new CreateGuestRequestValidator());
            validators.Add(typeof(ServiceModel), new CreateServiceRequestValidator());
            validators.Add(typeof(RoomModel), new CreateRoomRequestValidator());
            validators.Add(typeof(StaffModel), new CreateStaffRequestValidator());
            validators.Add(typeof(BookingRequestModel), new CreateBookingRequestValidator(hotelRedRepository,
                guestRedRepository, serviceRedRepository, roomRedRepository));
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new BookingValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
