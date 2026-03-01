using FluentValidation;
using PetDS.Contract;

namespace PetDS.Application.Locations.Commands.CreateLocation;

public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
{
    public CreateLocationValidator()
    {
        RuleFor(q => q.city).NotEmpty();
        RuleFor(q => q.namberHouse).NotEmpty();
        RuleFor(q => q.strit).NotEmpty();
        RuleFor(q => q.name).Length(3, 120).NotEmpty();
        RuleFor(q => q.region).NotEmpty();
    }
}