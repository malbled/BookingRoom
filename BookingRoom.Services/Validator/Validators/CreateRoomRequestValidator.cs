using BookingRoom.Services.Contracts.Models;
using FluentValidation;

namespace BookingRoom.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="RoomModel"/>
    /// </summary>
    public class CreateRoomRequestValidator : AbstractValidator<RoomModel>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 20).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.TypeRoom)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Description)
                .Length(3, 1000).WithMessage(MessageForValidation.LengthMessage).When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
