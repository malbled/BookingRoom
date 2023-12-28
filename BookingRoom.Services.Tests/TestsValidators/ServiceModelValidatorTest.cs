using BookingRoom.Services.Validator.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class ServiceModelValidatorTest
    {
        private readonly CreateServiceRequestValidator validator;

        public ServiceModelValidatorTest()
        {
            validator = new CreateServiceRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.ServiceModel(x => { x.Title = "а"; x.Description = "ааа";});

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
            var model = TestDataGenerator.ServiceModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
