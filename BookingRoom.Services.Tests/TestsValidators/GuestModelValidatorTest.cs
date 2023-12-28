using BookingRoom.Services.Validator.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class GuestModelValidatorTest
    {
        private readonly CreateGuestRequestValidator validator;

        public GuestModelValidatorTest()
        {
            validator = new CreateGuestRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel(x => { x.FirstName = "777"; x.LastName = "777"; x.MiddleName = "777"; x.Passport = "777"; x.AddressRegistration = "777"; });

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerator.GuestModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
