// <copyright file="UserRequestValidator.cs" company="dagaren">
// Copyright (c) dagaren. All rights reserved.
// </copyright>

namespace Example.API.Controllers.ViewModels.Validation
{
    using System;
    using System.Globalization;
    using FluentValidation;

    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        private const string DateFormat = "yyyy-MM-dd";

        private const string EmptyPropertyValidationMessage = "'{PropertyName}' cannot be empty";

        private const string InvalidDatePropertyValidationMessage = "'{PropertyName}' contains an invalid date. Expected format: yyyy-MM-dd";

        public UserRequestValidator()
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(EmptyPropertyValidationMessage);
            this.RuleFor(x => x.Birthdate)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(EmptyPropertyValidationMessage)
                .Must(b => DateTime.TryParseExact(b, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                .WithMessage(InvalidDatePropertyValidationMessage);
        }
    }
}
