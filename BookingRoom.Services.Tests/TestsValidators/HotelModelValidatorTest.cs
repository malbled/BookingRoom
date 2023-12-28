using BookingRoom.Services.Validator.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class HotelModelValidatorTest
    {
        private readonly CreateHotelRequestValidator validator;

        public HotelModelValidatorTest()
        {
            validator = new CreateHotelRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.HotelModel(x => { x.Title = "777"; x.Address = "777"; });

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
            var model = TestDataGenerator.HotelModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
