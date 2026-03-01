using FluentValidation;
using PetDS.Contract.Departamen;

namespace PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentLocations;

public class UpdateDepartamentLocationsValidator : AbstractValidator<UpdateDepartamentLocationsDto>
{
    public UpdateDepartamentLocationsValidator() => RuleFor(q => q.locationsId).NotEmpty()
        .Must(guids => guids.Distinct().Count() == guids.Count());
}