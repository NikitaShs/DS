using FluentValidation;
using PetDS.Contract.Departamen;

namespace PetDS.Application.Positions.PositionCreate;

public class PositionCreateValidator : AbstractValidator<PositionCreateDto>
{
    public PositionCreateValidator()
    {
        RuleFor(q => q.discription).MaximumLength(1000);
        RuleFor(q => q.name).Length(3, 100).NotEmpty();
        RuleFor(q => q.departamentId).NotEmpty().Must(guids => guids.Distinct().Count() == guids.Count);
    }
}