using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace DeliverySystem.Api.CommandValidators
{
    internal class PhoneNumberValidator : PropertyValidator
    {
        public PhoneNumberValidator(string errorMessage)
            : base(errorMessage)
        { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var phoneNumber = (string)context.PropertyValue;

            return phoneNumber.StartsWith("+44") && Regex.Replace(phoneNumber, @"[^0-9]+", "").Length == 12;
        }
    }
}