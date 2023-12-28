using BookingRoom.Services.Validator.Validators;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class RoomModelValidatorTest
    {
        private readonly CreateRoomRequestValidator validator;

        public RoomModelValidatorTest()
        {
            validator = new CreateRoomRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.RoomModel(x => { x.Title = "777";  x.TypeRoom = "7"; x.Description = "777"; });

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
            var model = TestDataGenerator.RoomModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
