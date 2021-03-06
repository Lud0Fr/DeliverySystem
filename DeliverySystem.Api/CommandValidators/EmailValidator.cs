﻿using FluentValidation.Validators;
using System;
using System.Text.RegularExpressions;

namespace DeliverySystem.Api.CommandValidators
{
    public class EmailValidator : PropertyValidator
    {
        public EmailValidator(string errorMessage)
            : base(errorMessage)
        { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var email = (string)context.PropertyValue;

            return Regex.IsMatch(email,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) &&
                email.Length <= 80;
        }
    }
}
