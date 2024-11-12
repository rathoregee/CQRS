using EventSourcing.Features.CreatePlayer;
using EventSourcing.Features.GetPlayerById;
using FluentValidation;
using System;

namespace EventSourcing.Validation
{
    public class CreatePlayerValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerValidator()
        {
            RuleFor(x => x.Level).LessThan(1).WithMessage("level is required and cannot be less than zero");
            RuleFor(x => x.Level).GreaterThan(int.MaxValue).WithMessage("value is greater than max value");
        }
    }
}