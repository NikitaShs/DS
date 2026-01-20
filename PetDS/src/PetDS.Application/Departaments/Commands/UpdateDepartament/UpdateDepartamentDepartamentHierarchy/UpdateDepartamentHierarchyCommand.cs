using Core.adstract;
using PetDS.Application.abcstractions;
using PetDS.Contract;

namespace PetDS.Application.Departaments.UpdateDepartament.UpdateDepartamentDepartamentHierarchy;

public record UpdateDepartamentHierarchyCommand(Guid departanetId, UpdateDepartamentHierarchyDto dto) : ICommand;