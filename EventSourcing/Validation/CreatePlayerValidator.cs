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
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Level).GreaterThan(0).WithMessage("level is required and cannot be less than zero");            
        }
    }
}