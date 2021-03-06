﻿using DeliverySystem.Api.Commands;
using FluentValidation;
using System;

namespace DeliverySystem.Api.CommandValidators
{
    public class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
    {
        public CreateDeliveryCommandValidator()
        {
            RuleFor(r => r.AccessWindow).NotNull();

            RuleFor(r => r.AccessWindow.StartTime).GreaterThan(DateTime.Now)
                .When(r => r.AccessWindow != null);

            RuleFor(r => r.AccessWindow.EndTime).GreaterThan(r => r.AccessWindow.StartTime)
                .When(r => r.AccessWindow != null);

            RuleFor(r => r.Recipient).NotNull();

            RuleFor(r => r.Recipient.Name).NotEmpty()
                .When(r => r.Recipient != null);

            RuleFor(r => r.Recipient.Name).MaximumLength(100)
                .When(r => r.Recipient != null);

            RuleFor(r => r.Recipient.Address).NotEmpty()
                .When(r => r.Recipient != null);

            RuleFor(r => r.Recipient.Email).NotEmpty()
                .When(r => r.Recipient != null);

            RuleFor(r => r.Recipient.Email).SetValidator(new EmailValidator("Not a valid email"))
                .When(r => r.Recipient != null && !string.IsNullOrEmpty(r.Recipient.Email));

            RuleFor(r => r.Recipient.PhoneNumber).NotEmpty()
                .When(r => r.Recipient != null);

            RuleFor(r => r.Recipient.PhoneNumber).SetValidator(new PhoneNumberValidator("Not a valid phone number"))
                .When(r => r.Recipient != null && !string.IsNullOrEmpty(r.Recipient.PhoneNumber));

            RuleFor(r => r.Order).NotNull();

            RuleFor(r => r.Order.OrderNumber).NotEmpty()
                .When(r => r.Order != null);

            RuleFor(r => r.Order.OrderNumber).MaximumLength(20)
                .When(r => r.Order != null);

            RuleFor(r => r.Order.Sender).NotEmpty()
                .When(r => r.Order != null);

            RuleFor(r => r.Order.Sender).MaximumLength(100)
                .When(r => r.Order != null);

            RuleFor(r => r.UserId).GreaterThan(0);
            RuleFor(r => r.PartnerId).GreaterThan(0);
        }
    }
}
