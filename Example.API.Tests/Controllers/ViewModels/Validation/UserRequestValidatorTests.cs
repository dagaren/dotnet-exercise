// <copyright file="UserRequestValidatorTests.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Tests.Controllers.ViewModels.Validation
{
    using Example.API.Controllers.ViewModels;
    using Example.API.Controllers.ViewModels.Validation;
    using FluentAssertions;
    using FluentValidation.Results;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserRequestValidatorTests
    {
        [TestMethod]
        public void Validate_ValidEntity_ValidReturned()
        {
            // Arrange
            var validatorSut = new UserRequestValidator();

            var userRequest = new UserRequest()
            {
                Birthdate = "1999-01-01",
                Name = "Name",
            };

            // Act
            ValidationResult result = validatorSut.Validate(userRequest);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [TestMethod]
        public void Validate_EmtpyName_InvalidReturned()
        {
            // Arrange
            var validatorSut = new UserRequestValidator();

            var userRequest = new UserRequest()
            {
                Birthdate = "1999-01-01",
                Name = string.Empty,
            };

            // Act
            ValidationResult result = validatorSut.Validate(userRequest);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Validate_EmtpyBirthdate_InvalidReturned()
        {
            // Arrange
            var validatorSut = new UserRequestValidator();

            var userRequest = new UserRequest()
            {
                Birthdate = string.Empty,
                Name = "Name",
            };

            // Act
            ValidationResult result = validatorSut.Validate(userRequest);

            // Assert
            result.IsValid.Should().BeFalse();
        }

        [TestMethod]
        public void Validate_BirthdateWithWrongFormat_InvalidReturned()
        {
            // Arrange
            var validatorSut = new UserRequestValidator();

            var userRequest = new UserRequest()
            {
                Birthdate = "1999/01/01",
                Name = "Name",
            };

            // Act
            ValidationResult result = validatorSut.Validate(userRequest);

            // Assert
            result.IsValid.Should().BeFalse();
        }
    }
}
