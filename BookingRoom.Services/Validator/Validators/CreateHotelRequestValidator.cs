using BookingRoom.Services.Contracts.Models;
using FluentValidation;

namespace BookingRoom.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="HotelModel"/>
    /// </summary>
    public class CreateHotelRequestValidator : AbstractValidator<HotelModel>
    {
        public CreateHotelRequestValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(3, 100).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(10, 200).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
