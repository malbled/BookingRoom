using BookingRoom.Services.Contracts.Models;
using FluentValidation;

namespace BookingRoom.Services.Validator.Validators
{
    /// <summary>
    /// Валидатор <see cref="ServiceModel"/>
    /// </summary>
    public class CreateServiceRequestValidator : AbstractValidator<ServiceModel>
    {
        public CreateServiceRequestValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 100).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Description)
                .Length(3, 1000).WithMessage(MessageForValidation.LengthMessage).When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
