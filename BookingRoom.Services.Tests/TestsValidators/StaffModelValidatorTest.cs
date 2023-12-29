using BookingRoom.Services.Contracts.Enums;
using BookingRoom.Services.Validator.Validators;
using BookingRoom.Test.Extensions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BookingRoom.Services.Tests.TestsValidators
{
    public class StaffModelValidatorTest
    {
        private readonly CreateStaffRequestValidator validator;

        public StaffModelValidatorTest()
        {
            validator = new CreateStaffRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorShouldError()
        {
            //Arrange
            var model = TestDataGenerator.StaffModel(x => { x.FirstName = "777"; x.LastName = "777"; x.MiddleName = "777"; x.Post = (PostModel)77; });

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
            var model = TestDataGenerator.StaffModel();

            // Act
            var result = validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
