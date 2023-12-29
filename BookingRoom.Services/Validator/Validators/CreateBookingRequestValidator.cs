using BookingRoom.Repositories.Contracts.ReadRepositoriesContracts;
using BookingRoom.Services.Contracts.ModelsRequest;
using FluentValidation;

namespace BookingRoom.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="BookingRequestModel"/>
    /// </summary>
    public class CreateBookingRequestValidator : AbstractValidator<BookingRequestModel>
    {
        private readonly IHotelRedRepository hotelRedRepository;
        private readonly IGuestRedRepository guestRedRepository;
        private readonly IServiceRedRepository serviceRedRepository;
        private readonly IRoomRedRepository roomRedRepository;

        public CreateBookingRequestValidator(IHotelRedRepository hotelRedRepository, IGuestRedRepository guestRedRepository,
            IServiceRedRepository serviceRedRepository, IRoomRedRepository roomRedRepository)
        {
            this.hotelRedRepository = hotelRedRepository;
            this.guestRedRepository = guestRedRepository;
            this.serviceRedRepository = serviceRedRepository;
            this.roomRedRepository = roomRedRepository;

            RuleFor(x => x.StaffId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .When(x => x.StaffId.HasValue);

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.roomRedRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.hotelRedRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.GuestId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.guestRedRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .MustAsync(async (x, cancellationToken) => await this.serviceRedRepository.IsNotNullAsync(x, cancellationToken))
                .WithMessage(MessageForValidation.NotFoundGuidMessage);

            RuleFor(x => x.DateCheckIn)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.DateCheckout)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage);

            RuleFor(x => x.Price)
                .InclusiveBetween(100, 10000000).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
