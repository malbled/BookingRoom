using BookingRoom.Services.Contracts.Models;
using FluentValidation;

namespace BookingRoom.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="StaffModel"/>
    /// </summary>
    public class CreateStaffRequestValidator : AbstractValidator<StaffModel>
    {
        public CreateStaffRequestValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.MiddleName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Post).IsInEnum().WithMessage(MessageForValidation.DefaultMessage);
        }
    }
}
