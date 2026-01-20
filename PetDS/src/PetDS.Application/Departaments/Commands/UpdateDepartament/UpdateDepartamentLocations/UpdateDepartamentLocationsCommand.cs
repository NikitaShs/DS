using Core.adstract;
using PetDS.Application.abcstractions;
using PetDS.Contract.Departamen;

namespace PetDS.Application.Departaments.Commands.UpdateDepartament.UpdateDepartamentLocations;

public record UpdateDepartamentLocationsCommand(UpdateDepartamentLocationsDto dto, Guid departamentId) : ICommand;